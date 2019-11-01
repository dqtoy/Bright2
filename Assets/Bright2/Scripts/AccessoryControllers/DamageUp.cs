using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.AccessoryControllers
{
    /// <summary>
    /// ダメージが上昇するアクセサリー効果のクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/Accessory Effect/DamageUp")]
    public sealed class DamageUp : AccessoryEffect, IDamageUp
    {
        [SerializeField][Range(0.0f, 1.0f)]
        private float rate = default;
        float IDamageUp.Rate => this.rate;
    }
}
