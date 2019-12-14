using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using UniRx;
using UnityEngine;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// ランダムな移動を行うAI
    /// </summary>
    [CreateAssetMenu(fileName = "RandomMove", menuName = "Bright2/AI/Elements/RandomMove")]
    public sealed class RandomMove : ScriptableAIElement
    {
        [SerializeField]
        private float moveSpeedRate = 1.0f;

        [SerializeField]
        private List<Vector2> velocities = default;

        private Vector2 velocity;

        public override void Enter(Actor owner, ActorAIController ownerAI)
        {
            base.Enter(owner, ownerAI);

            this.velocity = this.velocities[Random.Range(0, this.velocities.Count)];

            this.GetObserver(owner)
                .SubscribeWithState3(this, owner, ownerAI, (_, _this, _owner, _ownerAI) =>
                {
                    _owner.StatusController.SetMoveSpeedRate(_this.moveSpeedRate);
                    _owner.Broker.Publish(RequestMove.Get(_this.velocity));
                })
                .AddTo(this.events);
        }
    }
}
