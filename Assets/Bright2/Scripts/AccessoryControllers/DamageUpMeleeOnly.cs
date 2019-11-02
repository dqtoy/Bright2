using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.AccessoryControllers
{
    /// <summary>
    /// 近接武器によるダメージが上昇するアクセサリー効果のクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/Accessory Effect/DamageUpMeleeOnly")]
    public sealed class DamageUpMeleeOnly : AccessoryEffect
    {
        [SerializeField][Range(0.0f, 1.0f)]
        private float rate = default;

        public override void Give(ActorInstanceStatus.AccessoryEffectParameter parameter)
        {
            parameter.DamageUpMeleeOnly += this.rate;
        }
    }
}
