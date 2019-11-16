using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
using HK.Bright2.UIControllers;
using UnityEngine;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アイテム修飾のレシピマスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/ItemModifierRecipe/Record")]
    public sealed class ItemModifierRecipeRecord : MasterDataRecord, IMasterDataRecordId, IIconHolder, INameHolder, IViewableList
    {
        public string Id => this.name;

        public Sprite Icon => this.itemModifier.Icon;

        [SerializeField]
        private ItemModifier itemModifier = default;
        public ItemModifier ItemModifier => this.itemModifier;

        [SerializeField]
        private int money = default;

        [SerializeField]
        private NeedItems needItems = default;
        public NeedItems NeedItems => this.needItems;

        string INameHolder.Name => this.itemModifier.ItemModifierName;
    }
}
