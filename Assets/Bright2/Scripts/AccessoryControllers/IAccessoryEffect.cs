using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.AccessoryControllers
{
    /// <summary>
    /// アクセサリーの効果を持つインターフェイス
    /// </summary>
    public interface IAccessoryEffect
    {
        /// <summary>
        /// 効果を付与する
        /// </summary>
        void Give(ActorInstanceStatus.ItemEffectParameter parameter);
    }
}
