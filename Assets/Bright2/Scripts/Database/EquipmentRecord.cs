using System;
using HK.Bright2.GimmickControllers;
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

        [SerializeField]
        private Gimmick gimmick = default;
        public Gimmick Gimmick => this.gimmick;

        [SerializeField]
        private float coolTime = default;
        public float CoolTime => this.coolTime;
    }
}
