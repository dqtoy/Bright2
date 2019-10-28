using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えた際に追加で効果を与えるクラス
    /// </summary>
    public abstract class GiveDamageActorAdditionalEffect : ScriptableObject, IGiveDamageActorAdditionalEffect
    {
        public abstract void Do(IGiveDamage giveDamage);
    }
}
