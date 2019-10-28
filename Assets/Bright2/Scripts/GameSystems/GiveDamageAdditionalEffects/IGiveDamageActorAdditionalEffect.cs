using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えた際に追加で効果を与えるインターフェイス
    /// </summary>
    public interface IGiveDamageActorAdditionalEffect
    {
        void Do(IGiveDamage giveDamage);
    }
}
