using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// ジャンプステージを制御するクラス
    /// </summary>
    public sealed class Jump : ActorState
    {
        public Jump(Actor owner) : base(owner)
        {
        }

        public override void Enter()
        {
            this.owner.Movement.SetGravity(-this.owner.Context.JumpPower);
        }
    }
}
