using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.AccessoryControllers
{
    /// <summary>
    /// ダメージが上昇するアクセサリー効果のクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/Accessory Effect/DamageDown")]
    public sealed class DamageDown : AccessoryEffect
    {
        [SerializeField][Range(0.0f, 1.0f)]
        private float rate = default;

        public override void Give(ActorInstanceStatus.ItemEffectParameter parameter)
        {
        }
    }
}
