using System;
using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// ダメージを受けたら条件を満たす<see cref="ScriptableAICondition"/>
    /// </summary>
    [CreateAssetMenu(fileName = "TakeDamage", menuName = "Bright2/AI/Conditions/TakeDamage")]
    public sealed class TakeDamage : ScriptableAICondition
    {
        public override IObservable<Unit> Satisfy(Actor owner)
        {
            return owner.Broker.Receive<TakedDamage>().AsUnitObservable();
        }
    }
}
