using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
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

        public override void Enter(IActorStateContext context)
        {
            base.Enter(context);

            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Idle);

            this.owner.Broker.Receive<RequestMove>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Run, new StateRunContext(x.Direction));
                })
                .AddTo(this.events);

            this.owner.Broker.Receive<RequestJump>()
                .Where(_ => this.owner.StatusController.CanJump)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Jump);
                })
                .AddTo(this.events);

            this.owner.Broker.Receive<Messages.Fall>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StatusController.AddJumpCount();
                    _this.owner.StateManager.Change(ActorState.Name.Fall);
                })
                .AddTo(this.events);

            if(this.owner.StatusController.IsDash)
            {
                this.owner.Broker.Receive<Messages.Idle>()
                    .Take(1)
                    .SubscribeWithState(this, (_, _this) =>
                    {
                        _this.owner.StatusController.EndDash();
                    })
                    .AddTo(this.events);
            }
            
            this.ReceiveRequestFireOnChangeAttackState();
            this.ReceiveRequestInvokeGameEventOnInvokeGameEvent();
            this.ReceiveRequestFallOneWayPlatforms();
        }
    }
}
