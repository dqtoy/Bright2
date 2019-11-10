using System;
using HK.Bright2.GameSystems;
using HK.Bright2.GimmickControllers;
using HK.Bright2.ItemModifiers;
using HK.Bright2.MaterialControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アイテム修飾のレシピマスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/ItemModifierRecipe/Record")]
    public sealed class ItemModifierRecipeRecord : ScriptableObject, IMasterDataRecordId, IIconHolder
    {
        public string Id => this.name;

        public Sprite Icon => this.itemModifier.Icon;

        [SerializeField]
        private ItemModifier itemModifier = default;
        public ItemModifier ItemModifier => this.itemModifier;

        [SerializeField]
        private NeedMaterials needMaterials = default;
        public NeedMaterials NeedMaterials => this.needMaterials;
    }
}
