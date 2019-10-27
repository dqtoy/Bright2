using System.Collections.Generic;
using HK.Bright2.Database;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>の動的なステータス
    /// </summary>
    public sealed class ActorInstanceStatus
    {
        public ReactiveProperty<int> HitPoint { get; set; }

        public ReactiveProperty<int> HitPointMax { get; set; }

        /// <summary>
        /// ジャンプした回数
        /// </summary>
        public int JumpCount { get; set; }

        /// <summary>
        /// 装備中の装備品
        /// </summary>
        public List<EquippedEquipment> EquippedEquipments { get; private set; }

        /// <summary>
        /// 現在向いている方向
        /// </summary>
        public Constants.Direction Direction { get; set; }

        /// <summary>
        /// 所持金
        /// </summary>
        public int Money { get; set; }

        public ActorInstanceStatus(Actor owner, ActorContext context)
        {
            this.HitPoint = new ReactiveProperty<int>(context.BasicStatus.HitPoint);
            this.HitPointMax = new ReactiveProperty<int>(this.HitPoint.Value);
            this.Money = context.BasicStatus.Money;

            this.EquippedEquipments = new List<EquippedEquipment>();
            for (var i = 0; i < Constants.EquippedEquipmentMax; i++)
            {
                this.EquippedEquipments.Add(new EquippedEquipment(owner.gameObject));
            }
        }
    }
}
