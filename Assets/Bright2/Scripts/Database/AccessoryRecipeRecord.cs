using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
using HK.Bright2.UIControllers;
using UnityEngine;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アクセサリーのレシピマスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/AccessoryRecipe/Record")]
    public sealed class AccessoryRecipeRecord :
        MasterDataRecord,
        IMasterDataRecordId,
        IIconHolder,
        INameHolder,
        IViewableList,
        IMasterDataRecordRecipe
    {
        public string Id => this.name;

        public Sprite Icon => this.accessoryRecord.Icon;

        [SerializeField]
        private AccessoryRecord accessoryRecord = default;
        public AccessoryRecord AccessoryRecord => this.accessoryRecord;

        [SerializeField]
        private int money = default;
        public int Money => this.money;

        [SerializeField]
        private NeedItems needItems = default;
        public NeedItems NeedItems => this.needItems;

        string INameHolder.Name => this.accessoryRecord.AccessoryName;
    }
}
