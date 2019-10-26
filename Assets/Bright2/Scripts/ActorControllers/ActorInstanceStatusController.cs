using HK.Bright2.ActorControllers.Messages;
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

        public ActorInstanceStatusController(Actor owner, ActorContext context)
        {
            this.context = context;
            this.status = new ActorInstanceStatus(context);

            owner.Broker.Receive<Landed>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.ResetJumpCount();
                })
                .AddTo(owner);
        }

        public int JumpCount => this.status.JumpCount;

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
    }
}
