using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.AccessoryControllers
{
    /// <summary>
    /// 与えたダメージから回復するアクセサリー効果のクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/Accessory Effect/RecoveryOnGiveDamage")]
    public sealed class RecoveryOnGiveDamage : AccessoryEffect
    {
        [SerializeField][Range(0.0f, 1.0f)]
        private float rate = default;

        public override void Give(ActorInstanceStatus.AccessoryEffectParameter parameter)
        {
            parameter.RecoveryOnGiveDamage += this.rate;
        }
    }
}
