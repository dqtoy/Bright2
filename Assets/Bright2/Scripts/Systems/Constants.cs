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
            /// 数秒間ダメージを与える
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
        }

        /// <summary>
        /// 装備可能な武器の最大値
        /// </summary>
        public const int EquippedWeaponMax = 3;
    }
}
