using System;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 装備品マスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Equipment/Record")]
    public sealed class EquipmentRecord : ScriptableObject, IMasterDataRecordId
    {
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder equipmentName = default;
        public string EquipmentName => this.equipmentName.Get;
    }
}
