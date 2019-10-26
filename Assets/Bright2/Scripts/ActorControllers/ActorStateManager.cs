using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers.States;
using System.Collections.Generic;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="ActorState"/>を管理するクラス
    /// </summary>
    public sealed class ActorStateManager
    {
        private readonly Actor owner;

        private readonly Dictionary<ActorState.Name, IActorState> states = new Dictionary<ActorState.Name, IActorState>();

        private ActorState.Name currentState;

        public ActorStateManager(Actor owner)
        {
            this.owner = owner;

            this.states.Add(ActorState.Name.Idle, new Idle(this.owner));
            this.states.Add(ActorState.Name.Run, new Run(this.owner));
            this.states.Add(ActorState.Name.Jump, new Jump(this.owner));
            this.states.Add(ActorState.Name.Fall, new Fall(this.owner));

            this.currentState = ActorState.Name.Idle;
            this.states[this.currentState].Enter();
        }

        public void Change(ActorState.Name nextState)
        {
            Assert.IsTrue(this.states.ContainsKey(nextState));

            this.states[this.currentState].Exit();
            this.currentState = nextState;
            this.states[this.currentState].Enter();
        }
    }
}
