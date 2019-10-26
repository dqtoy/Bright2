using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>の動的なステータス
    /// </summary>
    public sealed class ActorInstanceStatus
    {
        /// <summary>
        /// ジャンプした回数
        /// </summary>
        public int JumpCount { get; set; }

        public ActorInstanceStatus(ActorContext context)
        {
        }
    }
}
