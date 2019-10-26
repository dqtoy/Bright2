using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// <see cref="Actor"/>のステートのインターフェイス
    /// </summary>
    public interface IActorState
    {
        void Enter();

        void Exit();
    }
}
