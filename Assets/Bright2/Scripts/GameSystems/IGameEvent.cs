using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ゲームイベントのインターフェイス
    /// </summary>
    public interface IGameEvent
    {
        /// <summary>
        /// ゲームイベントを実行する
        /// </summary>
        void Invoke(Actor invokedActor);
    }
}
