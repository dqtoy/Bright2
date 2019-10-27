using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers.States;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="ActorState.Name.Run"/>ステートで必要なデータを持つクラス
    /// </summary>
    public sealed class StateRunContext : IActorStateContext
    {
        public Vector2 Direction { get; }

        public StateRunContext(Vector2 direction)
        {
            this.Direction = direction;
        }
    }
}
