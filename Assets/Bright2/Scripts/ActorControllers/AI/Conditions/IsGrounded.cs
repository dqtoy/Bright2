using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 接地している場合に条件を満たす<see cref="ScriptableAICondition"/>
    /// </summary>
    [CreateAssetMenu(fileName = "Condition.IsGrounded.asset", menuName = "Bright2/AI/Conditions/IsGrounded")]
    public sealed class IsGrounded : ScriptableAICondition
    {
        /// <summary>
        /// 接地していない時に条件を満たすか
        /// </summary>
        [SerializeField]
        private bool isReverse = false;

        public override IObservable<Unit> Satisfy(Actor owner, ActorAIController ownerAI)
        {
            return owner.UpdateAsObservable().Where(_ => this.SatisfyFromDistance(owner));
        }

        private bool SatisfyFromDistance(Actor owner)
        {
            return owner.Movement.IsGrounded == !this.isReverse;
        }
    }
}
