using UnityEngine;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="Vector2"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 水平方向の<see cref="Constants.Direction"/>を返す
        /// </summary>
        public static Constants.Direction GetHorizontalDirection(this Vector2 self)
        {
            if(self.x == 0)
            {
                return Constants.Direction.None;
            }

            return self.x > 0.0f ? Constants.Direction.Right : Constants.Direction.Left;
        }
    }
}
