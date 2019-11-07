using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// <see cref="ISequenveGameEvent"/>の要素のインターフェイス
    /// </summary>
    public interface ISequenceGameEventElement
    {
        /// <summary>
        /// イベントの実行
        /// </summary>
        void Invoke(ISequenceGameEvent owner, Actor invoker);
    }
}
