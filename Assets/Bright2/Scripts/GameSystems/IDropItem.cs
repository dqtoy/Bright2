using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// アイテムをドロップするためのインターフェイス
    /// </summary>
    public interface IDropItem<T> : ILottery
    {
        /// <summary>
        /// 要素
        /// </summary>
        T Get { get; }
    }
}
