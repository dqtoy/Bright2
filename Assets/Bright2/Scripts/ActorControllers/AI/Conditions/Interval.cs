using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 一定間隔で条件を満たす<see cref="ScriptableAICondition"/>
    /// </summary>
    [CreateAssetMenu(fileName = "Condition.Interval.asset", menuName = "Bright2/AI/Conditions/Interval")]
    public sealed class Interval : ScriptableAICondition
    {
        [SerializeField]
        private float seconds = default;
        
        public override IObservable<Unit> Satisfy(Actor owner, ActorAIController ownerAI)
        {
            return Observable.Interval(TimeSpan.FromSeconds(this.seconds)).AsUnitObservable();
        }
    }
}
