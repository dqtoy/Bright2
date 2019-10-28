﻿using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
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
        /// 装備中の武器
        /// </summary>
        public List<EquippedEquipment> EquippedWeapons { get; private set; }

        /// <summary>
        /// 現在向いている方向
        /// </summary>
        public Constants.Direction Direction { get; set; }

        /// <summary>
        /// 所持金
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 所持している武器
        /// </summary>
        public List<WeaponRecord> PossessionWeapons { get; set; }

        /// <summary>
        /// 実行可能なゲームイベント
        /// </summary>
        public IGameEvent GameEvent { get; set; }

        public ActorInstanceStatus(Actor owner, ActorContext context)
        {
            this.HitPoint = new ReactiveProperty<int>(context.BasicStatus.HitPoint);
            this.HitPointMax = new ReactiveProperty<int>(this.HitPoint.Value);
            this.Money = context.BasicStatus.Money;

            this.EquippedWeapons = new List<EquippedEquipment>();
            for (var i = 0; i < Constants.EquippedWeaponMax; i++)
            {
                this.EquippedWeapons.Add(new EquippedEquipment(owner.gameObject));
            }
        }
    }
}
