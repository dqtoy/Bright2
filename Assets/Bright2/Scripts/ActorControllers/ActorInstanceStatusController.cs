using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
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

        public List<EquippedEquipment> EquippedEquipments => this.status.EquippedEquipments;

        public Constants.Direction Direction => this.status.Direction;

        public int Money => this.status.Money;

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

        public void SetEquipment(int index, EquipmentRecord equipment)
        {
            this.status.EquippedEquipments[index].Change(equipment);
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

        public void AddEquipment(EquipmentRecord equipment)
        {
            this.status.PossessionEquipments = this.status.PossessionEquipments ?? new List<EquipmentRecord>();
            this.status.PossessionEquipments.Add(equipment);
        }
    }
}
