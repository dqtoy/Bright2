using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
using HK.Bright2.StageControllers.Messages;
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

        public Inventory Inventory => this.status.Inventory;

        private IDisposable lackOfOxygenDamageStream;

        public IReactiveProperty<int> HitPoint => this.status.HitPoint;

        public IReactiveProperty<int> HitPointMax => this.status.HitPointMax;

        public int JumpCount => this.status.JumpCount;

        public List<EquippedWeapon> EquippedWeapons => this.status.EquippedWeapons;

        public IReadOnlyList<int> EquippedAccessories => this.status.EquippedAccessories;

        public IReadOnlyList<IIconHolder> EquippedAccessoryIcons => this.status.EquippedAccessoryIcons;

        public Constants.Direction Direction => this.status.Direction;

        public IGameEvent GameEvent => this.status.GameEvent;

        public ActorInstanceStatus.ItemModifierEffectParameter ItemModifierEffect => this.status.ItemModifierEffect;

        public float MoveSpeed => this.context.BasicStatus.MoveSpeed * this.status.MoveSpeedRate;

        public float KnockbackResistance => this.context.BasicStatus.KnockbackResistance;

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

                return this.JumpCount < (this.context.BasicStatus.LimitJumpCount + this.ItemModifierEffect.Get(Constants.ItemModifierType.AddJumpCount));
            }
        }

        public bool IsDash
        {
            get
            {
                return this.status.MoveSpeedRate > 1.0f;
            }
        }

        public void ChangeEquippedWeapon(int index, InstanceWeapon instanceWeapon)
        {
            if(instanceWeapon != null)
            {
                Assert.IsTrue(this.Inventory.Weapons.Contains(instanceWeapon), $"所持していない武器を装備しようとしました");
            }

            var tempEquippedWeapon = this.status.EquippedWeapons[index];

            // すでに装備している箇所があったら外す
            for (var i = 0; i < this.status.EquippedWeapons.Count; i++)
            {
                var e = this.status.EquippedWeapons[i];
                if (e.InstanceWeapon == instanceWeapon)
                {
                    e.Change(tempEquippedWeapon.InstanceWeapon);
                    this.owner.Broker.Publish(ChangedEquippedWeapon.Get(i));
                    break;
                }
            }

            this.status.EquippedWeapons[index].Change(instanceWeapon);
            this.owner.Broker.Publish(ChangedEquippedWeapon.Get(index));

            this.ResetItemModifierParameter();
        }

        public void RemoveEquippedWeapon(int index)
        {
            this.status.EquippedWeapons[index].Change(null);
            this.owner.Broker.Publish(ChangedEquippedWeapon.Get(index));
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
            this.Inventory.AddMoney(value);
            this.owner.Broker.Publish(AcquiredMoney.Get(value));
        }

        public void AddWeapon(WeaponRecord weaponRecord)
        {
            var instanceWeapon = this.Inventory.AddWeapon(weaponRecord);

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

        public void AddAccessory(AccessoryRecord accessoryRecord)
        {
            this.Inventory.AddAccessory(accessoryRecord);
            this.owner.Broker.Publish(AcquiredAccessory.Get(accessoryRecord));
        }

        public void AddMaterial(MaterialRecord materialRecord, int amount)
        {
            this.Inventory.AddMaterial(materialRecord, amount);
            this.owner.Broker.Publish(AcquiredMaterial.Get(materialRecord));
        }

        public void AddImportantItem(ImportantItemRecord importantItemRecord)
        {
            this.Inventory.AddImportantItem(importantItemRecord);
            this.owner.Broker.Publish(AcquiredImportantItem.Get(importantItemRecord));
            this.ResetItemModifierParameter();
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

        public void ChangeEquippedAccessory(int equippedAccessoryIndex, int possessionAccessoryIndex)
        {
            Assert.IsTrue(equippedAccessoryIndex >= 0 && equippedAccessoryIndex < Constants.EquippedAccessoryMax);
            Assert.IsTrue(possessionAccessoryIndex < this.Inventory.Accessories.Count);

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

        /// <summary>
        /// 装備中のアクセサリーのインデックスを減算する
        /// </summary>
        /// <remarks>
        /// 所持しているアクセサリーが消費された場合にインデックスがずれるため減算する必要があります
        /// </remarks>
        public void DecreaseEquippedAccessoryIndex(int removedPossessionAccessoryIndex)
        {
            for (var i = 0; i < this.status.EquippedAccessories.Length; i++)
            {
                if(this.status.EquippedAccessories[i] > removedPossessionAccessoryIndex)
                {
                    this.status.EquippedAccessories[i]--;
                }
            }
        }

        public void AttachItemModifierToInstanceWeapon(InstanceWeapon instanceWeapon, IItemModifier itemModifier)
        {
            Assert.IsTrue(this.Inventory.Weapons.Contains(instanceWeapon), "所持していない武器です");
            instanceWeapon.Modifiers.Add(itemModifier);

            this.ResetItemModifierParameter();
        }

        public void StartDash()
        {
            this.SetMoveSpeedRate(2.0f);
        }

        public void EndDash()
        {
            this.ResetMoveSpeedRate();
        }

        public void SetMoveSpeedRate(float value)
        {
            this.status.MoveSpeedRate = value;
        }

        public void ResetMoveSpeedRate()
        {
            this.status.MoveSpeedRate = 1.0f;
        }

        public void AddOtherItemModifier(IItemModifier itemModifier)
        {
            this.status.OtherItemModifiers.Add(itemModifier);
            this.ResetItemModifierParameter();
        }

        public void RemoveOtherItemModifier(IItemModifier itemModifier)
        {
            this.status.OtherItemModifiers.Remove(itemModifier);
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

                var accessoryRecord = this.Inventory.Accessories[i];
                foreach (var m in accessoryRecord.Modifiers)
                {
                    m.Give(this.status.ItemModifierEffect);
                }
            }

            foreach(var i in this.Inventory.ImportantItems)
            {
                foreach(var m in i.Modifiers)
                {
                    m.Give(this.status.ItemModifierEffect);
                }
            }

            foreach (var i in this.status.OtherItemModifiers)
            {
                i.Give(this.status.ItemModifierEffect);
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
