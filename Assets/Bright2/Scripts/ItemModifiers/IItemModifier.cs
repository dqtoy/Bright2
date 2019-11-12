using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ItemModifiers
{
    /// <summary>
    /// アイテムに何かしらの効果を付与するインターフェイス
    /// </summary>
    public interface IItemModifier
    {
        Constants.ItemModifierType Type { get; }

        int Amount { get; }
        
        void Give(ActorInstanceStatus.ItemModifierEffectParameter parameter);
    }
}
