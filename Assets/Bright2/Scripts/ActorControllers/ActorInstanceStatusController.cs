using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems;
using HK.Bright2.ItemModifiers;
using HK.Bright2.MaterialControllers;
using HK.Bright2.StageControllers.Messages;
using HK.Bright2.WeaponControllers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="ActorInstanceStatus"/>を制御するクラス
    /// </summary>
    public sealed class ActorInstanceStatusController
    {
        private readonly ActorContext context;

        private readonly ActorInstanceStatus status;

        private readonly Actor owner;

        private IDisposable lackOfOxygenDamageStream;

        public IReactiveProperty<int> HitPoint => this.status.HitPoint;

        public IReactiveProperty<int> HitPointMax => this.status.HitPointMax;

        public int JumpCount => this.status.JumpCount;

        public List<EquippedWeapon> EquippedWeapons => this.status.EquippedWeapons;

        public IReadOnlyList<int> EquippedAccessories => this.status.EquippedAccessories;

        public IReadOnlyList<IIconHolder> EquippedAccessoryIcons => this.status.EquippedAccessoryIcons;

        public Constants.Direction Direction => this.status.Direction;

        public int Money => this.status.Money;

        public IGameEvent GameEvent => this.status.GameEvent;

        public IReadOnlyList<InstanceWeapon> PossessionWeapons => this.status.PossessionWeapons;

        public IReadOnlyList<AccessoryRecord> PossessionAccessories => this.status.PossessionAccessories;

        public ActorInstanceStatus.ItemModifierEffectParameter ItemModifierEffect => this.status.ItemModifierEffect;

        public IReadOnlyDictionary<MaterialRecord, InstanceMaterial> PossessionMaterials
        {
            get
            {
                this.status.PossessionMaterials = this.status.PossessionMaterials ?? new Dictionary<MaterialRecord, InstanceMaterial>();
                return this.status.PossessionMaterials;
            }
        }

        public ActorInstanceStatusController(Actor owner, ActorContext context)
        {
            this.owner = owner;
            this.context = context;
            this.status = new ActorInstanceStatus(owner, context);
            this.status.Direction = Constants.Direction.Right;

            this.status.EquippedAccessories = new int[Constants.EquippedAccessoryMax];
            for (var i = 0; i < this.status.EquippedAccessories.Length; i++)
            {
                this.status.EquippedAccessories[i] = -1;
            }

            owner.Broker.Receive<Landed>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.ResetJumpCount();
                })
                .AddTo(owner);

            owner.Broker.Receive<EnterUnderWater>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.RegisterUpdateUnderWaterSecondsStream();
                })
                .AddTo(owner);

            owner.Broker.Receive<ExitUnderWater>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.status.EnterUnderWaterSeconds = 0.0f;
                    if(_this.lackOfOxygenDamageStream != null)
                    {
                        _this.lackOfOxygenDamageStream.Dispose();
                        _this.lackOfOxygenDamageStream = null;
                    }
                })
                .AddTo(owner);
        }

        public void AddJumpCount()
        {
            this.status.JumpCount++;
        }

        public void ResetJumpCount()
        {
            this.status.JumpCount = 0;
        }

        public bool CanJump
        {
            get
            {
                // 水中にいたら何度でもジャンプ出来る
                if(this.status.IsEnterUnderWater)
                {
                    return true;
                }

                return this.JumpCount < this.context.BasicStatus.LimitJumpCount;
            }
        }

        public void ChangeEquippedWeapon(int index, InstanceWeapon instanceWeapon)
        {
            Assert.IsTrue(this.status.PossessionWeapons.Contains(instanceWeapon), $"所持していない武器を装備しようとしました");

            this.status.EquippedWeapons[index].Change(instanceWeapon);
            this.owner.Broker.Publish(ChangedEquippedWeapon.Get(index));

            this.ResetItemModifierParameter();
        }

        public void SetDirection(Constants.Direction direction)
        {
            this.status.Direction = direction;
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        public void TakeDamage(DamageResult damageResult)
        {
            // すでに死亡していたら何もしない
            if(this.status.HitPoint.Value <= 0)
            {
                return;
            }

            this.status.HitPoint.Value -= damageResult.Damage;
            this.owner.Broker.Publish(TakedDamage.Get(damageResult));

            // 与えた側にも通知する
            damageResult.Attacker?.Broker.Publish(GivedDamage.Get(damageResult));

            // 死亡したら通知する
            if(this.status.HitPoint.Value <= 0)
            {
                this.owner.Broker.Publish(Died.Get(damageResult.Attacker));
            }
        }

        public void AddMoney(int value)
        {
            this.status.Money += value;
            this.owner.Broker.Publish(AcquiredMoney.Get(value));
        }

        public void AddWeapon(WeaponRecord weapon)
        {
            this.status.PossessionWeapons = this.status.PossessionWeapons ?? new List<InstanceWeapon>();
            var instanceWeapon = new InstanceWeapon(weapon);
            this.status.PossessionWeapons.Add(instanceWeapon);

            // 装備していない箇所があったら自動的に装備する
            for (var i = 0; i < this.status.EquippedWeapons.Count; i++)
            {
                var e = this.status.EquippedWeapons[i];
                if (!e.IsEquipped)
                {
                    this.ChangeEquippedWeapon(i, instanceWeapon);
                    break;
                }
            }

            this.owner.Broker.Publish(AcquiredWeapon.Get(instanceWeapon));
        }

        public void SetGameEvent(IGameEvent gameEvent)
        {
            this.status.GameEvent = gameEvent;
        }

        public void AddInfinityStatus(GameObject attackedObject, float infinitySeconds)
        {
            var instanceId = attackedObject.GetInstanceID();

            if(!this.status.InfinityStatuses.ContainsKey(instanceId))
            {
                this.status.InfinityStatuses.Add(instanceId, new InfinityStatus(this.owner, instanceId, infinitySeconds));
            }
            else
            {
                var infinityStatus = this.status.InfinityStatuses[instanceId];
                if(infinityStatus.IsInfinity)
                {
                    Debug.Assert(false, $"無敵中なのに{attackedObject.name}の攻撃が通りました", attackedObject);
                }

                infinityStatus.SetInfinitySeconds(infinitySeconds);
            }
        }

        public bool IsInfinity(GameObject attackedObject)
        {
            var instanceId = attackedObject.GetInstanceID();

            if(!this.status.InfinityStatuses.ContainsKey(instanceId))
            {
                return false;
            }

            return this.status.InfinityStatuses[instanceId].IsInfinity;
        }

        public void AddAccessory(AccessoryRecord accessoryRecord)
        {
            this.status.PossessionAccessories = this.status.PossessionAccessories ?? new List<AccessoryRecord>();
            this.status.PossessionAccessories.Add(accessoryRecord);
            this.owner.Broker.Publish(AcquiredAccessory.Get(accessoryRecord));
        }

        public void ChangeEquippedAccessory(int equippedAccessoryIndex, int possessionAccessoryIndex)
        {
            Assert.IsTrue(equippedAccessoryIndex >= 0 && equippedAccessoryIndex < Constants.EquippedAccessoryMax);
            Assert.IsTrue(possessionAccessoryIndex >= 0 && possessionAccessoryIndex < this.status.PossessionAccessories.Count);

            // 同じものを装備しようとした場合は外す
            if(this.status.EquippedAccessories[equippedAccessoryIndex] == possessionAccessoryIndex)
            {
                this.status.EquippedAccessories[equippedAccessoryIndex] = -1;
            }
            else
            {
                // 他の箇所で同じものを装備している場合は外す
                for (var i = 0; i < this.status.EquippedAccessories.Length; i++)
                {
                    if (this.status.EquippedAccessories[i] == possessionAccessoryIndex)
                    {
                        this.status.EquippedAccessories[i] = -1;
                    }
                }

                this.status.EquippedAccessories[equippedAccessoryIndex] = possessionAccessoryIndex;
            }

            this.ResetItemModifierParameter();
        }

        public void AddMaterial(MaterialRecord materialRecord, int amount)
        {
            this.status.PossessionMaterials = this.status.PossessionMaterials ?? new Dictionary<MaterialRecord, InstanceMaterial>();
            var instance = default(InstanceMaterial);
            if(!this.status.PossessionMaterials.TryGetValue(materialRecord, out instance))
            {
                instance = new InstanceMaterial(materialRecord);
                this.status.PossessionMaterials.Add(materialRecord, instance);
            }

            instance.Add(amount);
            this.owner.Broker.Publish(AcquiredMaterial.Get(materialRecord));
        }

        public void AttachItemModifierToInstanceWeapon(InstanceWeapon instanceWeapon, IItemModifier itemModifier)
        {
            Assert.IsTrue(this.status.PossessionWeapons.Contains(instanceWeapon), "所持していない武器です");
            instanceWeapon.Modifiers.Add(itemModifier);

            this.ResetItemModifierParameter();
        }

        private void RegisterUpdateUnderWaterSecondsStream()
        {
            this.owner.UpdateAsObservable()
                .TakeUntil(this.owner.Broker.Receive<ExitUnderWater>())
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.status.EnterUnderWaterSeconds += Time.deltaTime;

                    if(_this.status.IsLackOfOxygen && _this.lackOfOxygenDamageStream == null)
                    {
                        _this.lackOfOxygenDamageStream = _this.RegisterLackOfOxygenDamageStream();
                    }
                })
                .AddTo(this.owner);
        }

        private IDisposable RegisterLackOfOxygenDamageStream()
        {
            return Observable.Interval(TimeSpan.FromSeconds(Constants.LackOfOxygenDamageSeconds))
                .TakeUntil(this.owner.Broker.Receive<ExitUnderWater>())
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.TakeDamage(Calculator.GetDamageResultOnLackOfOxygen(_this.owner));
                })
                .AddTo(this.owner);
        }

        private void ResetItemModifierParameter()
        {
            this.status.ItemModifierEffect.Reset();

            foreach(var w in this.status.EquippedWeapons)
            {
                if(w.InstanceWeapon == null)
                {
                    continue;
                }

                foreach(var i in w.InstanceWeapon.Modifiers)
                {
                    i.Give(this.status.ItemModifierEffect);
                }
            }

            foreach (var i in this.status.EquippedAccessories)
            {
                if (i == -1)
                {
                    continue;
                }

                var accessoryRecord = this.status.PossessionAccessories[i];
                foreach (var m in accessoryRecord.Modifiers)
                {
                    m.Give(this.status.ItemModifierEffect);
                }
            }

            var hitPoint = this.context.BasicStatus.HitPoint;
            this.status.HitPointMax.Value =
                hitPoint +
                Mathf.FloorToInt(hitPoint * this.ItemModifierEffect.GetPercent(Constants.ItemModifierType.HitPointUpRate)) +
                this.ItemModifierEffect.Get(Constants.ItemModifierType.HitPointUpFixed);
            if(this.status.HitPoint.Value > this.status.HitPointMax.Value)
            {
                this.status.HitPoint.Value = this.status.HitPointMax.Value;
            }
        }
    }
}
