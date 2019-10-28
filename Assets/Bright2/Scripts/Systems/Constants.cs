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

        /// <summary>
        /// 装備可能な武器の最大値
        /// </summary>
        public const int EquippedWeaponMax = 3;
    }
}
