using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// <see cref="Actor"/>のステートの抽象クラス
    /// </summary>
    public abstract class ActorState : IActorState
    {
        /// <summary>
        /// ステートの名前
        /// </summary>
        public enum Name
        {
            Idle,
            Run,
            Attack,
            Jump,
        }

        protected readonly Actor owner;

        protected readonly CompositeDisposable events = new CompositeDisposable();

        public ActorState(Actor owner)
        {
            this.owner = owner;
        }

        public abstract void Enter();

        public virtual void Exit()
        {
            this.events.Clear();
        }
    }
}
