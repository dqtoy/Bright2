using System;
using System.Collections.Generic;
using HK.Bright2.AccessoryControllers;
using HK.Bright2.GameSystems;
using HK.Bright2.GimmickControllers;
using HK.Bright2.UIControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アクセサリーマスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Accessory/Record")]
    public sealed class AccessoryRecord : MasterDataRecord, IMasterDataRecordId, IIconHolder, INameHolder, IViewableList
    {
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder accessoryName = default;
        public string AccessoryName => this.accessoryName.Get;

        [SerializeField]
        private Sprite icon = default;
        public Sprite Icon => this.icon;

        [SerializeField]
        private List<AccessoryEffect> effects = default;
        public List<AccessoryEffect> Effects => this.effects;

        string INameHolder.Name => this.AccessoryName;
    }
}
