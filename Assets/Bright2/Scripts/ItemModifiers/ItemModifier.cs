using HK.Bright2.GameSystems;
using HK.Bright2.MaterialControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ItemModifiers
{
    /// <summary>
    /// アイテムに何かしらの効果を付与するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/ItemModifiers/ItemModifier")]
    public sealed class ItemModifier : ScriptableObject, IIconHolder
    {
        [SerializeField]
        private Sprite icon = default;
        public Sprite Icon => this.icon;
        
        [SerializeField]
        private Constants.ItemModifierType type = default;
        public Constants.ItemModifierType Type => this.type;

        [SerializeField]
        private float amount = default;
        public float Amount => this.amount;
    }
}
