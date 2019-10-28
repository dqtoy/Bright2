using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems;
using UniRx;
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
        }

        public IReactiveProperty<int> HitPoint => this.status.HitPoint;

        public IReactiveProperty<int> HitPointMax => this.status.HitPointMax;

        public int JumpCount => this.status.JumpCount;

        public List<EquippedWeapon> EquippedWeapons => this.status.EquippedWeapons;

        public Constants.Direction Direction => this.status.Direction;

        public int Money => this.status.Money;

        public IGameEvent GameEvent => this.status.GameEvent;

        public IReadOnlyList<WeaponRecord> PossessionWeapons => this.status.PossessionWeapons;

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
                return this.JumpCount < this.context.BasicStatus.LimitJumpCount;
            }
        }

        public void SetWeapon(int index, WeaponRecord weapon)
        {
            this.status.EquippedWeapons[index].Change(weapon);
        }

        public void SetDirection(Constants.Direction direction)
        {
            this.status.Direction = direction;
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        public void TakeDamage(int damage, Vector3 generationSource)
        {
            // すでに死亡していたら何もしない
            if(this.status.HitPoint.Value <= 0)
            {
                return;
            }

            this.status.HitPoint.Value -= damage;

            this.owner.Broker.Publish(TakedDamage.Get(damage, generationSource));

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
            this.status.PossessionWeapons = this.status.PossessionWeapons ?? new List<WeaponRecord>();
            this.status.PossessionWeapons.Add(weapon);
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
    }
}
