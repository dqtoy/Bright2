using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// 重み付けを行って抽選するインターフェイス
    /// </summary>
    public interface IWeightLottery
    {
        /// <summary>
        /// 重み
        /// </summary>
        int Weight { get; }
    }
}
