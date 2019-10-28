using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に武器を追加するインターフェイス
    /// </summary>
    public interface IAddWeapon
    {
        /// <summary>
        /// 初期化
        /// </summary>
        void Setup(WeaponRecord weapon);
    }
}
