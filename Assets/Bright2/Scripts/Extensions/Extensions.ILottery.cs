using UnityEngine;
using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using HK.Bright2.GameSystems.Messages;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="ILottery"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static bool Lottery(this ILottery self)
        {
            return self.WinningRate.Lottery();
        }
    }
}
