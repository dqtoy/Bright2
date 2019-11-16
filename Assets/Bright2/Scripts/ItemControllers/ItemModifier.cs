using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using HK.Framework.Text;
using UnityEngine;

namespace HK.Bright2.ItemControllers
{
    /// <summary>
    /// アイテムに何かしらの効果を付与するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/ItemModifiers/ItemModifier")]
    public sealed class ItemModifier : ScriptableObject, IItemModifier, IIconHolder
    {
        [SerializeField]
        private Sprite icon = default;
        public Sprite Icon => this.icon;

        [SerializeField]
        private StringAsset.Finder itemModifierName = default;
        public string ItemModifierName => this.itemModifierName.Format(this.Amount);

        [SerializeField]
        private Constants.ItemModifierType type = default;
        public Constants.ItemModifierType Type => this.type;

        [SerializeField]
        private int amount = default;
        public int Amount => this.amount;

        public void Give(ActorInstanceStatus.ItemModifierEffectParameter parameter)
        {
            parameter.Add(this.type, this.amount);
        }
    }
}
