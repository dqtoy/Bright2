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

        public int JumpCount => this.status.JumpCount;

        public List<EquippedEquipment> EquippedEquipments => this.status.EquippedEquipments;

        public Constants.Direction Direction => this.status.Direction;

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

        public void TakeDamage(int damage)
        {
            // すでに死亡していたら何もしない
            if(this.status.HitPoint <= 0)
            {
                return;
            }

            this.status.HitPoint -= damage;

            // 死亡したら通知する
            if(this.status.HitPoint <= 0)
            {
                this.owner.Broker.Publish(Died.Get());
            }
        }
    }
}
