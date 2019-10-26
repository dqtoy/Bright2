using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Run"/>ステートで必要なデータを持つクラス
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
