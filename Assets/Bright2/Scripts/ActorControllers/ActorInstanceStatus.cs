using System;
using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.WeaponControllers;
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
        public List<EquippedWeapon> EquippedWeapons { get; private set; }

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
        public List<InstanceWeapon> PossessionWeapons { get; set; }

        /// <summary>
        /// 実行可能なゲームイベント
        /// </summary>
        public IGameEvent GameEvent { get; set; }

        /// <summary>
        /// 水中にいる時間（秒）
        /// </summary>
        public float EnterUnderWaterSeconds { get; set; }

        /// <summary>
        /// 水中にいるか返す
        /// </summary>
        public bool IsEnterUnderWater => this.EnterUnderWaterSeconds > 0.0f;

        /// <summary>
        /// 酸欠中であるか返す
        /// </summary>
        public bool IsLackOfOxygen => this.EnterUnderWaterSeconds >= Constants.LackOfOxygenSeconds;

        /// <summary>
        /// 無敵ステータス
        /// </summary>
        public readonly Dictionary<int, InfinityStatus> InfinityStatuses = new Dictionary<int, InfinityStatus>();

        /// <summary>
        /// 装備中のアクセサリーのインデックスリスト
        /// </summary>
        /// <remarks>
        /// <see cref="PossessionAccessories"/>の添字と紐付いています
        /// </remarks>
        public int[] EquippedAccessories { get; set; }

        /// <summary>
        /// 所持しているアクセサリー
        /// </summary>
        public List<AccessoryRecord> PossessionAccessories { get; set; }

        /// <summary>
        /// アクセサリー効果
        /// </summary>
        public AccessoryEffectParameter AccessoryEffect { get; set; }

        public ActorInstanceStatus(Actor owner, ActorContext context)
        {
            this.HitPoint = new ReactiveProperty<int>(context.BasicStatus.HitPoint);
            this.HitPointMax = new ReactiveProperty<int>(this.HitPoint.Value);
            this.Money = context.BasicStatus.Money;

            this.EquippedWeapons = new List<EquippedWeapon>();
            for (var i = 0; i < Constants.EquippedWeaponMax; i++)
            {
                this.EquippedWeapons.Add(new EquippedWeapon(owner.gameObject));
            }
        }

        /// <summary>
        /// アクセサリー効果
        /// </summary>
        [Serializable]
        public class AccessoryEffectParameter
        {
            /// <summary>
            /// ダメージの上昇倍率
            /// </summary>
            public float DamageUp;
        }
    }
}
