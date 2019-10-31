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
            Fall,
            Knockback,
        }

        protected readonly Actor owner;

        protected readonly CompositeDisposable events = new CompositeDisposable();

        protected IActorStateContext context;

        Actor IActorState.Owner => this.owner;

        CompositeDisposable IActorState.Events => this.events;

        public ActorState(Actor owner)
        {
            this.owner = owner;
        }

        public virtual void Enter(IActorStateContext context)
        {
            this.context = context;
        }

        public virtual void Exit()
        {
            this.events.Clear();
        }
    }
}
