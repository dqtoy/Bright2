using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// アイドル状態を制御するクラス
    /// </summary>
    public sealed class Idle : ActorState
    {
        public Idle(Actor owner)
            :base(owner)
        {
        }

        public override void Enter()
        {
            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Idle);

            this.owner.Broker.Receive<Move>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Run);
                })
                .AddTo(this.events);


            this.owner.Broker.Receive<RequestJump>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Jump);
                })
                .AddTo(this.events);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
