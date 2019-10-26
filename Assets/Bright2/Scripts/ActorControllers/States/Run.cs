using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// ラン状態を制御するクラス
    /// </summary>
    public sealed class Run : ActorState
    {
        public Run(Actor owner)
            :base(owner)
        {
        }

        public override void Enter()
        {
            this.owner.AnimationController.StartSequence(this.owner.Context.Run);

            this.owner.Broker.Receive<Messages.Idle>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Idle);
                })
                .AddTo(this.events);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
