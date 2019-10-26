using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>が持つモデルの名前を管理するクラス
    /// </summary>
    public static class ActorModelNames
    {
        public static readonly int Idle0 = Animator.StringToHash("Idle0");
        public static readonly int Idle1 = Animator.StringToHash("Idle1");

        public static readonly int Fall0 = Animator.StringToHash("Fall0");

        public static readonly int Attack0 = Animator.StringToHash("Attack0");

        public static readonly int Jump0 = Animator.StringToHash("Jump0");

        public static readonly int Run0 = Animator.StringToHash("Run0");
        public static readonly int Run1 = Animator.StringToHash("Run1");
        public static readonly int Run2 = Animator.StringToHash("Run2");
    }
}
