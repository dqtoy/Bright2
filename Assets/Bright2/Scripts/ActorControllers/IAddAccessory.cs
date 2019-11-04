using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>にアクセサリーを追加するインターフェイス
    /// </summary>
    public interface IAddAccessory
    {
        /// <summary>
        /// 初期化
        /// </summary>
        void Setup(AccessoryRecord accessory);
    }
}
