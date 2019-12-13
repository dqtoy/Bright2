using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 接地した時に条件を満たす<see cref="ScriptableAICondition"/>
    /// </summary>
    [CreateAssetMenu(fileName = "Condition.Landed.asset", menuName = "Bright2/AI/Conditions/Landed")]
    public sealed class Landed : ScriptableAICondition
    {
        public override IObservable<Unit> Satisfy(Actor owner, ActorAIController ownerAI)
        {
            return owner.Broker.Receive<Messages.Landed>().AsUnitObservable();
        }
    }
}
