using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// <see cref="ScriptableObject"/>で定義可能な<see cref="IAICondition"/>
    /// </summary>
    public abstract class ScriptableAICondition : ScriptableObject, IAICondition
    {
        public abstract IObservable<Unit> Satisfy();
    }
}
