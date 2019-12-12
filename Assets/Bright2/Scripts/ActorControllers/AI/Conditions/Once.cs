using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 一度だけ条件を満たす<see cref="ScriptableAICondition"/>
    /// </summary>
    [CreateAssetMenu(fileName = "Condition.Once.asset", menuName = "Bright2/AI/Conditions/Once")]
    public sealed class Once : ScriptableAICondition
    {
        public override IObservable<Unit> Satisfy(Actor owner, ActorAIController ownerAI)
        {
            return owner.UpdateAsObservable().Take(1);
        }
    }
}
