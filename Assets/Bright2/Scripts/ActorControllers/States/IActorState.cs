using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// <see cref="Actor"/>のステートのインターフェイス
    /// </summary>
    public interface IActorState
    {
        Actor Owner { get; }

        CompositeDisposable Events { get; }

        void Enter(IActorStateContext context);

        void Exit(ActorState.Name nextState);
    }
}
