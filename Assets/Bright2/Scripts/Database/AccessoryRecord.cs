using System.Collections.Generic;
using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
using HK.Bright2.UIControllers;
using HK.Framework.Text;
using UnityEngine;

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
        private List<ItemModifier> modifiers = default;
        public List<ItemModifier> Modifiers => this.modifiers;

        string INameHolder.Name => this.AccessoryName;
    }
}
