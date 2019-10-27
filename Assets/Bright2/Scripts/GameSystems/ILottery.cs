using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// 抽選するインターフェイス
    /// </summary>
    public interface ILottery
    {
        /// <summary>
        /// 当選確率
        /// 0 = 当たらない
        /// 1 = 必ず当たる
        /// </summary>
        float WinningRate { get; }

        /// <summary>
        /// 抽選に当選したか返す
        /// </summary>
        bool IsWinning { get; }
    }
}
