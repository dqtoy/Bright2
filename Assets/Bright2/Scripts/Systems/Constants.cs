using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// ゲーム中に利用する定数
    /// </summary>
    public class Constants
    {
        public enum Direction
        {
            None,

            Left,

            Right,

            Up,

            Down,
        }

        public enum AbnormalStatus
        {
            /// <summary>
            /// 数秒間ダメージを受ける
            /// </summary>
            Poison,

            /// <summary>
            /// 数秒間移動出来ない
            /// </summary>
            Paralysis,

            /// <summary>
            /// 数秒間移動方向が逆になる
            /// </summary>
            Confuse,

            /// <summary>
            /// 数秒間攻撃出来なくなる
            /// </summary>
            Fear,

            /// <summary>
            /// 数秒間超ダメージを受ける
            /// </summary>
            DeadlyPoison,

            /// <summary>
            /// 数秒間ダメージを受けて与えるダメージも減る
            /// </summary>
            FireSpread,
        }

        /// <summary>
        /// ダメージの発生源
        /// </summary>
        public enum DamageSource
        {
            /// <summary>
            /// <see cref="Actor"/>によるダメージ
            /// </summary>
            Actor,

            /// <summary>
            /// 状態異常によるダメージ
            /// </summary>
            AbnormalStatus,

            /// <summary>
            /// 酸欠によるダメージ
            /// </summary>
            LackOfOxygen,
        }

        public enum WeaponType
        {
            /// <summary>
            /// 近接武器
            /// </summary>
            Melee,

            /// <summary>
            /// 遠距離武器
            /// </summary>
            /// <remarks>
            /// 敵の遠距離攻撃の判定で利用します
            /// </remarks>
            LongRange,

            /// <summary>
            /// 弓
            /// </summary>
            Bow,

            /// <summary>
            /// 銃
            /// </summary>
            Gun,

            /// <summary>
            /// 魔法
            /// </summary>
            Magic,

            /// <summary>
            /// 召喚
            /// </summary>
            Summon,
        }

        /// <summary>
        /// アイテム修飾のタイプ
        /// </summary>
        public enum ItemModifierType
        {
            HitPointUpFixed,
            HitPointUpRate,
            GiveDamageUpFixed,
            GiveDamageUpRate,
            GiveDamageUpRateMeleeOnly,
            TakeDamageDownFixed,
            TakeDamageDownRate,
            CriticalUpRate,
            DropMoneyUpRate,
            DropWeaponAndAccessoryRate,
            DropMaterialRate,
            CoolTimeDownRate,
            FireSpeedUpRate,
            WeaponDamageUpFixed,
            WeaponDamageUpRate,
            RecoveryOnGiveDamage,
        }

        public enum FadeType
        {
            ToRight,
            ToLeft,
        }

        /// <summary>
        /// 装備可能な武器の最大値
        /// </summary>
        public const int EquippedWeaponMax = 3;

        /// <summary>
        /// ノックバックする時間（秒）
        /// </summary>
        public const float KnockbackDuration = 0.5f;

        /// <summary>
        /// 酸欠になるまでの時間（秒）
        /// </summary>
        public const float LackOfOxygenSeconds = 10.0f;

        /// <summary>
        /// 酸欠によるダメージを受けるまでの時間（秒）
        /// </summary>
        public const float LackOfOxygenDamageSeconds = 0.333f;

        /// <summary>
        /// 酸欠によるダメージの割合
        /// </summary>
        public const float LackOfOxygenDamageRate = 0.1f;

        /// <summary>
        /// アクセサリーを装備出来る最大値
        /// </summary>
        public const int EquippedAccessoryMax = 5;

        /// <summary>
        /// フレームレート
        /// </summary>
        public const int TargetFrameRate = 60;
    }
}
