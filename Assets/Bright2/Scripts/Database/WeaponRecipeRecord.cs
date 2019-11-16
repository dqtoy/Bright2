using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
using HK.Bright2.UIControllers;
using UnityEngine;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アイテム修飾のレシピマスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/WeaponRecipe/Record")]
    public sealed class WeaponRecipeRecord : MasterDataRecord, IMasterDataRecordId, IIconHolder, INameHolder, IViewableList
    {
        public string Id => this.name;

        public Sprite Icon => this.weaponRecord.Icon;

        [SerializeField]
        private WeaponRecord weaponRecord = default;
        public WeaponRecord WeaponRecord => this.weaponRecord;

        [SerializeField]
        private NeedItems needMaterials = default;
        public NeedItems NeedMaterials => this.needMaterials;

        string INameHolder.Name => this.weaponRecord.WeaponName;
    }
}
