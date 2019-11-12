using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="float"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static bool Lottery(this float self)
        {
            Assert.IsTrue(self >= 0.0f);
            return self >= Random.value;
        }
    }
}
