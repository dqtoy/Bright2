using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// <see cref="SequenceGameEvent"/>の要素の抽象クラス
    /// </summary>
    public abstract class SequenceGameEventElement : ScriptableObject, ISequenceGameEventElement
    {
        public abstract void Invoke(ISequenceGameEvent owner, Actor invoker);
    }
}
