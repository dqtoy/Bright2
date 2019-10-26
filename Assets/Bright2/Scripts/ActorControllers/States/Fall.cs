using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// 落下ステートを制御するクラス
    /// </summary>
    public sealed class Fall : ActorState
    {
        public Fall(Actor owner) : base(owner)
        {
        }

        public override void Enter()
        {
            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Fall);

            this.ReceiveRequestMoveOnMove();
            
            this.owner.Broker.Receive<Landed>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Idle);
                })
                .AddTo(this.events);

            this.owner.Broker.Receive<RequestJump>()
                .Where(_ => this.owner.StatusController.CanJump)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Jump);
                })
                .AddTo(this.events);
        }
    }
}
