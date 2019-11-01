using System;
using System.Collections.Generic;
using HK.Bright2.AccessoryControllers;
using HK.Bright2.GimmickControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アクセサリーマスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Accessory/Record")]
    public sealed class AccessoryRecord : ScriptableObject, IMasterDataRecordId
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
    }
}
