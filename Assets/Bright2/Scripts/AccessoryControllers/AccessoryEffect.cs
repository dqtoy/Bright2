using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.AccessoryControllers
{
    /// <summary>
    /// アクセサリーの効果を持つ抽象クラス
    /// </summary>
    public abstract class AccessoryEffect : ScriptableObject, IAccessoryEffect
    {
        public abstract void Give(ActorInstanceStatus.AccessoryEffectParameter parameter);
    }
}
