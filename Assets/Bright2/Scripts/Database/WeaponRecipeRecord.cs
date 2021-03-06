﻿using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
using HK.Bright2.UIControllers;
using UnityEngine;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 武器のレシピマスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/WeaponRecipe/Record")]
    public sealed class WeaponRecipeRecord :
        MasterDataRecord,
        IMasterDataRecordId,
        IIconHolder,
        INameHolder,
        IViewableList,
        IMasterDataRecordRecipe
    {
        public string Id => this.name;

        public Sprite Icon => this.weaponRecord.Icon;

        [SerializeField]
        private WeaponRecord weaponRecord = default;
        public WeaponRecord WeaponRecord => this.weaponRecord;

        [SerializeField]
        private int money = default;
        public int Money => this.money;

        [SerializeField]
        private NeedItems needItems = default;
        public NeedItems NeedItems => this.needItems;

        string INameHolder.Name => this.weaponRecord.WeaponName;
    }
}
