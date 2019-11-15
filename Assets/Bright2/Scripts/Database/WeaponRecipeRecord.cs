using System;
using HK.Bright2.GameSystems;
using HK.Bright2.GimmickControllers;
using HK.Bright2.ItemModifiers;
using HK.Bright2.MaterialControllers;
using HK.Bright2.UIControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

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
        private NeedMaterials needMaterials = default;
        public NeedMaterials NeedMaterials => this.needMaterials;

        string INameHolder.Name => this.weaponRecord.WeaponName;
    }
}
