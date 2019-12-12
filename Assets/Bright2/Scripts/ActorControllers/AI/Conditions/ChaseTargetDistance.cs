using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// <see cref="ActorAIController.ChaseTarget"/>との距離から条件を満たす<see cref="ScriptableAICondition"/>
    /// </summary>
    [CreateAssetMenu(fileName = "Condition.ChaseTargetDistance.asset", menuName = "Bright2/AI/Conditions/ChaseTargetDistance")]
    public sealed class ChaseTargetDistance : ScriptableAICondition
    {
        public enum CompareType
        {
            Greater,
            Less,
        }

        [SerializeField]
        private float distance = default;

        [SerializeField]
        private CompareType type = default;

        public override IObservable<Unit> Satisfy(Actor owner, ActorAIController ownerAI)
        {
            return owner.UpdateAsObservable().Where(_ => this.SatisfyFromDistance(owner, ownerAI));
        }

        private bool SatisfyFromDistance(Actor owner, ActorAIController ownerAI)
        {
            var diff = (ownerAI.ChaseTarget.CachedTransform.position - owner.CachedTransform.position).sqrMagnitude;
            var d = this.distance * this.distance;
            switch(this.type)
            {
                case CompareType.Greater:
                    return d < diff;
                case CompareType.Less:
                    return d > diff;
                default:
                    Assert.IsTrue(false, $"{this.type}は未対応です");
                    return false;
            }
        }
    }
}
