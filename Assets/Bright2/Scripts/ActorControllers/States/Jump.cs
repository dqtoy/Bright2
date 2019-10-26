using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// ジャンプステージを制御するクラス
    /// </summary>
    public sealed class Jump : ActorState
    {
        public Jump(Actor owner) : base(owner)
        {
        }

        public override void Enter()
        {
            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Jump);
            this.InvokeJump();

            this.owner.Broker.Receive<RequestJump>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.InvokeJump();
                })
                .AddTo(this.events);

            this.owner.Broker.Receive<Landed>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Idle);
                })
                .AddTo(this.events);
        }

        public override void Exit()
        {
            base.Exit();
            this.owner.StatusController.ResetJumpCount();
        }

        private void InvokeJump()
        {
            if(!this.owner.StatusController.CanJump)
            {
                return;
            }

            this.owner.StatusController.AddJumpCount();
            this.owner.Movement.SetGravity(-this.owner.Context.BasicStatus.JumpPower);
        }
    }
}
