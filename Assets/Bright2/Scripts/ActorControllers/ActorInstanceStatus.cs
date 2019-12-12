using System;
using System.Collections.Generic;
using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
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
        /// 実行可能なゲームイベント
        /// </summary>
        public IGameEvent GameEvent { get; set; }

        /// <summary>
        /// 水中にいる時間（秒）
        /// </summary>
        public float EnterUnderWaterSeconds { get; set; }

        /// <summary>
        /// 移動速度の倍率
        /// </summary>
        public float MoveSpeedRate { get; set; } = 1.0f;

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

        public Inventory Inventory { get; private set; }

        /// <summary>
        /// 装備中のアクセサリーのインデックスリスト
        /// </summary>
        /// <remarks>
        /// <see cref="Inventory.Accessories"/>の添字と紐付いています
        /// </remarks>
        public int[] EquippedAccessories { get; set; }

        /// <summary>
        /// 外部から受け付けた<see cref="IItemModifier"/>のリスト
        /// </summary>
        /// <remarks>
        /// 敵の一時的なステータス変更で利用しています
        /// </remarks>
        public readonly List<IItemModifier> OtherItemModifiers = new List<IItemModifier>();

        /// <summary>
        /// アイテム修飾効果
        /// </summary>
        public ItemModifierEffectParameter ItemModifierEffect { get; } = new ItemModifierEffectParameter();

        public readonly ActorPrefs Prefs = new ActorPrefs();

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

                    result[i] = this.Inventory.Accessories[this.EquippedAccessories[i]];
                }

                return result;
            }
        }

        public ActorInstanceStatus(Actor owner, ActorContext context)
        {
            this.HitPoint = new ReactiveProperty<int>(context.BasicStatus.HitPoint);
            this.HitPointMax = new ReactiveProperty<int>(this.HitPoint.Value);
            this.Inventory = new Inventory(owner, context);

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
        public class ItemModifierEffectParameter
        {
            private readonly Dictionary<Constants.ItemModifierType, int> parameters = new Dictionary<Constants.ItemModifierType, int>();

            public void Reset()
            {
                this.parameters.Clear();
            }

            public void Add(Constants.ItemModifierType type, int value)
            {
                if (!this.parameters.ContainsKey(type))
                {
                    this.parameters.Add(type, value);
                }
                else
                {
                    this.parameters[type] += value;
                }
            }

            public int Get(Constants.ItemModifierType type)
            {
                return this.parameters.ContainsKey(type) ? this.parameters[type] : 0;
            }

            public float GetPercent(Constants.ItemModifierType type)
            {
                return this.parameters.ContainsKey(type) ? (this.parameters[type] / 100.0f) : 0.0f;
            }

            public bool Contains(Constants.ItemModifierType type)
            {
                return this.parameters.ContainsKey(type);
            }
        }
    }
}
