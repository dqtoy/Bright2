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
        private float enterTime;

        private Vector2 lastDirection;

        public Run(Actor owner)
            :base(owner)
        {
            this.enterTime = Time.realtimeSinceStartup;
        }

        public override void Enter(IActorStateContext context)
        {
            base.Enter(context);

            var stateRunContext = (StateRunContext)context;

            var newEnterTime = Time.realtimeSinceStartup;
            var diffTime = newEnterTime - this.enterTime;
            if (this.CanDash(stateRunContext.Direction, newEnterTime))
            {
                this.owner.StatusController.StartDash();
            }

            this.enterTime = newEnterTime;

            this.lastDirection = stateRunContext.Direction;

            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Run);
            
            this.AddMove(stateRunContext.Direction, () => this.owner.StatusController.MoveSpeed);

            this.ReceiveRequestMoveOnMove(() => this.owner.StatusController.MoveSpeed);

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

        public override void Exit(Name nextState)
        {
            base.Exit(nextState);

            if(nextState == Name.Idle && this.owner.StatusController.IsDash)
            {
                // 次のRunステートではダッシュしない
                this.enterTime = 0.0f;
                this.owner.StatusController.EndDash();
            }
        }

        private bool CanDash(Vector2 direction, float enterTime)
        {
            if(!this.owner.StatusController.ItemModifierEffect.Contains(Constants.ItemModifierType.Dash))
            {
                return false;
            }
            
            if(this.lastDirection.x > 0 && direction.x > 0 || this.lastDirection.x < 0 && direction.x < 0)
            {
                var diffTime = enterTime - this.enterTime;
                return diffTime < Constants.DashReceptionTime;
            }

            return false;
        }
    }
}
