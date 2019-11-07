using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// 一連の流れを持つゲームイベントのインターフェイス
    /// </summary>
    public interface ISequenceGameEvent : IGameEvent
    {
        /// <summary>
        /// イベントの要素リスト
        /// </summary>
        ISequenceGameEventElement[] Elements { get; }
    }
}
