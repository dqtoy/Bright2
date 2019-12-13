using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// プレイヤーを追いかけるだけのAI
    /// </summary>
    [CreateAssetMenu(fileName = "ChasePlayer", menuName = "Bright2/AI/Elements/ChasePlayer")]
    public sealed class ChasePlayer : ScriptableAIElement
    {
        /// <summary>
        /// 追いかけるか
        /// </summary>
        /// <remarks>
        /// <c>false</c>の場合は逃げる動作になります
        /// </remarks>
        [SerializeField]
        private bool isChase = true;

        [SerializeField]
        private float changeDirectionDelay = default;

        [SerializeField]
        private float moveSpeedRate = 1.0f;

        private Constants.Direction currentDirection;

        private float changeDirectionDuration = 0.0f;

        public override void Enter(Actor owner, ActorAIController ownerAI)
        {
            base.Enter(owner, ownerAI);

            ownerAI.ChaseTarget = GameSystem.Instance.ActorManager.GetRandomPlayer();
            this.currentDirection = this.GetTargetDirection(owner, ownerAI);

            this.GetObserver(owner)
                .SubscribeWithState3(this, owner, ownerAI, (_, _this, _owner, _ownerAI) =>
                {
                    _owner.StatusController.SetMoveSpeedRate(_this.moveSpeedRate);
                    _owner.Broker.Publish(RequestMove.Get(_this.currentDirection.ToVector2()));

                    if(_this.IsReverse(_owner, _ownerAI))
                    {
                        _this.changeDirectionDuration += Time.deltaTime;
                    }
                    else
                    {
                        _this.changeDirectionDuration = 0.0f;
                    }

                    if(_this.changeDirectionDuration > _this.changeDirectionDelay)
                    {
                        _this.changeDirectionDuration = 0.0f;
                        _this.currentDirection = _this.GetTargetDirection(_owner, _ownerAI);
                    }
                })
                .AddTo(this.events);
        }

        private Constants.Direction GetTargetDirection(Actor owner, ActorAIController ownerAI)
        {
            var result = new Vector2(ownerAI.ChaseTarget.CachedTransform.position.x - owner.CachedTransform.position.x, 0.0f).GetHorizontalDirection();
            if(!this.isChase)
            {
                result = result.ToReverse();
            }

            return result;
        }

        private bool IsReverse(Actor owner, ActorAIController ownerAI)
        {
            var direction = owner.StatusController.Direction;
            if(!this.isChase)
            {
                direction = direction.ToReverse();
            }
            
            var diff = ownerAI.ChaseTarget.CachedTransform.position.x - owner.CachedTransform.position.x;
            if((direction == Constants.Direction.Left && diff > 0.0f) || (direction == Constants.Direction.Right && diff < 0.0f))
            {
                return true;
            }

            return false;
        }
    }
}
