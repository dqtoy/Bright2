﻿using UniRx;
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
        }

        protected readonly Actor owner;

        protected readonly CompositeDisposable events = new CompositeDisposable();

        Actor IActorState.Owner => this.owner;

        CompositeDisposable IActorState.Events => this.events;

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
