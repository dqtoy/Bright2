using System;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// ノックバック状態を制御するクラス
    /// </summary>
    public sealed class Knockback : ActorState
    {
        public Knockback(Actor owner)
            :base(owner)
        {
        }

        public override void Enter(IActorStateContext context)
        {
            base.Enter(context);
            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Knockback);

            // 着地もしくは指定した時間になったらノックバック終了
            Observable.Merge(
                this.owner.Broker.Receive<Landed>().AsUnitObservable(),
                Observable.Timer(TimeSpan.FromSeconds(Constants.KnockbackDuration)).AsUnitObservable()
            )
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.StateManager.Change(ActorState.Name.Idle);
                })
                .AddTo(this.events);
        }
    }
}
