using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
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

        public override void Enter(IActorStateContext context)
        {
            base.Enter(context);

            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Run);
            
            var stateRunContext = (StateRunContext)context;
            this.AddMove(stateRunContext.Direction, this.owner.Context.BasicStatus.MoveSpeed);

            this.ReceiveRequestMoveOnMove(this.owner.Context.BasicStatus.MoveSpeed);

            this.owner.Broker.Receive<Messages.Idle>()
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

            this.owner.Broker.Receive<Messages.Fall>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StatusController.AddJumpCount();
                    _this.owner.StateManager.Change(ActorState.Name.Fall);
                })
                .AddTo(this.events);

            this.ReceiveRequestFireOnChangeAttackState();
            this.SyncActorDirection();
            this.ReceiveRequestFallOneWayPlatforms();
        }
    }
}
