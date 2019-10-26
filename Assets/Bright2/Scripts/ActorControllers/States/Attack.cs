using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// 攻撃状態を制御するクラス
    /// </summary>
    public sealed class Attack : ActorState
    {
        private float nextStateDelaySeconds;

        public Attack(Actor owner)
            :base(owner)
        {
        }

        public override void Enter()
        {
            this.nextStateDelaySeconds = 0.0f;
            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Attack);
        }
    }
}
