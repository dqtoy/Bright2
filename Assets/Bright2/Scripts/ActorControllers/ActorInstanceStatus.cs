using System;
using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.MaterialControllers;
using HK.Bright2.WeaponControllers;
using UniRx;

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
        public ItemEffectParameter AccessoryEffect { get; } = new ItemEffectParameter();

        /// <summary>
        /// 所持している素材リスト
        /// </summary>
        public Dictionary<MaterialRecord, InstanceMaterial> PossessionMaterials { get; set; }

        public IReadOnlyList<IIconHolder> EquippedAccessoryIcons
        {
            get
            {
                // this.EquippedAccessories = this.EquippedAccessories ?? new int[Constants.EquippedAccessoryMax];
                var result = new IIconHolder[this.EquippedAccessories.Length];
                for (var i = 0; i < this.EquippedAccessories.Length; i++)
                {
                    var a = this.EquippedAccessories[i];
                    if(a == -1)
                    {
                        result[i] = EmptyIconHolder.Default;
                        continue;
                    }

                    result[i] = this.PossessionAccessories[this.EquippedAccessories[i]];
                }

                return result;
            }
        }

        public ActorInstanceStatus(Actor owner, ActorContext context)
        {
            this.HitPoint = new ReactiveProperty<int>(context.BasicStatus.HitPoint);
            this.HitPointMax = new ReactiveProperty<int>(this.HitPoint.Value);
            this.Money = context.BasicStatus.Money;

            this.EquippedWeapons = new List<EquippedWeapon>();
            for (var i = 0; i < Constants.EquippedWeaponMax; i++)
            {
                this.EquippedWeapons.Add(new EquippedWeapon(owner));
            }
        }

        /// <summary>
        /// アイテムによるパラメータ変化をまとめるクラス
        /// </summary>
        [Serializable]
        public class ItemEffectParameter
        {
            /// <summary>
            /// 与えるダメージの上昇倍率
            /// </summary>
            public float DamageUp;

            /// <summary>
            /// 受けるダメージの減少倍率
            /// </summary>
            public float DamageDown;

            /// <summary>
            /// 攻撃速度の上昇倍率
            /// </summary>
            public float FireSpeedUp;

            /// <summary>
            /// 近接武器による与えるダメージの上昇倍率
            /// </summary>
            public float DamageUpMeleeOnly;

            /// <summary>
            /// 与えたダメージから回復を行う割合
            /// </summary>
            public float RecoveryOnGiveDamage;

            public void Reset()
            {
                this.DamageUp = 0.0f;
                this.DamageDown = 0.0f;
                this.FireSpeedUp = 0.0f;
                this.DamageUpMeleeOnly = 0.0f;
                this.RecoveryOnGiveDamage = 0.0f;
            }
        }
    }
}
