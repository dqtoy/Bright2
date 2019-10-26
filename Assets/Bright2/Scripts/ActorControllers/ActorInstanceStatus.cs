using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>の動的なステータス
    /// </summary>
    public sealed class ActorInstanceStatus
    {
        public int HitPoint { get; set; }

        public int HitPointMax { get; set; }

        /// <summary>
        /// ジャンプした回数
        /// </summary>
        public int JumpCount { get; set; }

        /// <summary>
        /// 装備中の装備品
        /// </summary>
        public EquipmentRecord equipmentRecord { get; set; }

        /// <summary>
        /// 現在向いている方向
        /// </summary>
        public Constants.Direction Direction { get; set; }

        public ActorInstanceStatus(ActorContext context)
        {
            this.HitPoint = context.BasicStatus.HitPoint;
            this.HitPointMax = this.HitPoint;
        }
    }
}
