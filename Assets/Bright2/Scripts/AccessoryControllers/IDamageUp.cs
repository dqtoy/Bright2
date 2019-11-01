using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.AccessoryControllers
{
    /// <summary>
    /// ダメージが上昇するアクセサリーのインターフェイス
    /// </summary>
    public interface IDamageUp
    {
        /// <summary>
        /// 上昇する割合
        /// </summary>
        float Rate { get; }
    }
}
