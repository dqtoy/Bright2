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

        public ActorInstanceStatusController(ActorContext context)
        {
            this.context = context;
            this.status = new ActorInstanceStatus(context);
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
