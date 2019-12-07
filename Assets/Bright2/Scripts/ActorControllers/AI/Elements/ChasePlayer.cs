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

        private Actor target;

        private Constants.Direction currentDirection;

        private float changeDirectionDuration = 0.0f;

        public override void Enter(Actor owner)
        {
            base.Enter(owner);

            this.target = GameSystem.Instance.ActorManager.GetRandomPlayer();
            this.currentDirection = this.GetTargetDirection(owner);

            this.GetObserver(owner)
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    _owner.Broker.Publish(RequestMove.Get(_this.currentDirection.ToVector2()));

                    if(_this.IsReverse(_owner))
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
                        _this.currentDirection = _this.GetTargetDirection(_owner);
                    }
                })
                .AddTo(this.events);
        }

        private Constants.Direction GetTargetDirection(Actor owner)
        {
            var result = new Vector2(this.target.CachedTransform.position.x - owner.CachedTransform.position.x, 0.0f).GetHorizontalDirection();
            if(!this.isChase)
            {
                result = result.ToReverse();
            }

            return result;
        }

        private bool IsReverse(Actor owner)
        {
            var direction = owner.StatusController.Direction;
            if(!this.isChase)
            {
                direction = direction.ToReverse();
            }
            
            var diff = this.target.CachedTransform.position.x - owner.CachedTransform.position.x;
            if((direction == Constants.Direction.Left && diff > 0.0f) || (direction == Constants.Direction.Right && diff < 0.0f))
            {
                return true;
            }

            return false;
        }
    }
}
