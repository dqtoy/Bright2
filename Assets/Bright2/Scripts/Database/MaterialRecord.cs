using System;
using HK.Bright2.GameSystems;
using HK.Bright2.GimmickControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 素材マスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Material/Record")]
    public sealed class MaterialRecord : ScriptableObject, IMasterDataRecordId, IIconHolder
    {
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder materialName = default;
        public string MaterialName => this.materialName.Get;
        
        [SerializeField]
        private Sprite icon = default;
        public Sprite Icon => this.icon;
    }
}
