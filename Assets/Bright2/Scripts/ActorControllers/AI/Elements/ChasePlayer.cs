using HK.Bright2.ActorControllers.Messages;
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
    [CreateAssetMenu(fileName = "ChasePlayer", menuName = "Bright2/AI/ChasePlayer")]
    public sealed class ChasePlayer : ScriptableAI
    {
        private Actor target;

        public override void Enter(Actor owner)
        {
            this.target = GameSystem.Instance.ActorManager.GetRandomPlayer();

            owner.UpdateAsObservable()
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    var horizontal = _this.target.CachedTransform.position.x - _owner.CachedTransform.position.x;
                    _owner.Broker.Publish(RequestMove.Get(new Vector2(horizontal, 0.0f)));
                })
                .AddTo(this.events);
        }
    }
}
