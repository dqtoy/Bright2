using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// ラン状態を制御するクラス
    /// </summary>
    public sealed class Run : ActorState
    {
        public Run(Actor owner)
            :base(owner)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
