using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems;
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

        public Constants.Direction Direction => this.status.Direction;

        public int Money => this.status.Money;

        public IGameEvent GameEvent => this.status.GameEvent;

        public IReadOnlyList<InstanceWeapon> PossessionWeapons => this.status.PossessionWeapons;

        public ActorInstanceStatus.AccessoryEffectParameter AccessoryEffect => this.status.AccessoryEffect;

        public ActorInstanceStatusController(Actor owner, ActorContext context)
        {
            this.owner = owner;
            this.context = context;
            this.status = new ActorInstanceStatus(owner, context);
            this.status.Direction = Constants.Direction.Right;

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

            // 死亡したら通知する
            if(this.status.HitPoint.Value <= 0)
            {
                this.owner.Broker.Publish(Died.Get());
            }
        }

        public void AddMoney(int value)
        {
            this.status.Money += value;
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
        }

        public void ChangeEquippedAccessory(int equippedAccessoryIndex, int possessionAccessoryIndex)
        {
            Assert.IsTrue(equippedAccessoryIndex >= 0 && equippedAccessoryIndex < Constants.EquippedAccessoryMax);
            Assert.IsTrue(possessionAccessoryIndex >= 0 && possessionAccessoryIndex < this.status.PossessionAccessories.Count);

            if(this.status.EquippedAccessories == null)
            {
                this.status.EquippedAccessories = new int[Constants.EquippedAccessoryMax];
                for (var i = 0; i < this.status.EquippedAccessories.Length; i++)
                {
                    this.status.EquippedAccessories[i] = -1;
                }
            }

            this.status.EquippedAccessories[equippedAccessoryIndex] = possessionAccessoryIndex;

            this.status.AccessoryEffect.Reset();
            foreach(var i in this.status.EquippedAccessories)
            {
                if(i == -1)
                {
                    continue;
                }

                var accessoryRecord = this.status.PossessionAccessories[i];
                foreach(var e in accessoryRecord.Effects)
                {
                    e.Give(this.status.AccessoryEffect);
                }
            }
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
    }
}
