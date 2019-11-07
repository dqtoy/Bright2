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
        /// 次のイベントへ進む
        /// </summary>
        void Next(ISequenceGameEventElement nextEvent);
    }
}
