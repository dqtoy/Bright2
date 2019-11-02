using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers.States;
using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using UniRx;

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

            this.states.Add(ActorState.Name.Idle, new States.Idle(this.owner));
            this.states.Add(ActorState.Name.Run, new Run(this.owner));
            this.states.Add(ActorState.Name.Jump, new Jump(this.owner));
            this.states.Add(ActorState.Name.Fall, new States.Fall(this.owner));
            this.states.Add(ActorState.Name.Attack, new Attack(this.owner));
            this.states.Add(ActorState.Name.Knockback, new Knockback(this.owner));

            this.currentState = ActorState.Name.Idle;
            this.states[this.currentState].Enter(null);

            this.AnyEnterState();
        }

        public void Change(ActorState.Name nextState, IActorStateContext context)
        {
            Assert.IsTrue(this.states.ContainsKey(nextState), $"{nextState}に対応した{typeof(IActorState)}が存在しません");

            this.states[this.currentState].Exit();
            this.currentState = nextState;
            this.states[this.currentState].Enter(context);
        }

        public void Change(ActorState.Name nextState)
        {
            this.Change(nextState, null);
        }

        /// <summary>
        /// 全てのステート共通で遷移するステート処理
        /// </summary>
        public void AnyEnterState()
        {
            // ダメージを受けたら強制でノックバックステートへ遷移
            this.owner.Broker.Receive<TakedDamage>()
                .Where(x => x.Result.DamageSource == Constants.DamageSource.Actor)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Change(ActorState.Name.Knockback);
                })
                .AddTo(this.owner);
        }
    }
}
