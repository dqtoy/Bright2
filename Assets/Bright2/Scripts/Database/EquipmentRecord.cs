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
        private float nextStateDelaySeconds = default;
        /// <summary>
        /// 攻撃状態から次の状態へ移動するまでの遅延時間（秒）
        /// </summary>
        public float NextStateDelaySeconds => this.nextStateDelaySeconds;

        [SerializeField]
        private float coolTimeSeconds = default;
        /// <summary>
        /// 攻撃出来るまでのクールタイム（秒）
        /// </summary>
        public float CoolTimeSeconds => this.coolTimeSeconds;

        [SerializeField][Range(0.0f, 1.0f)]
        private float moveSpeedAttenuationRate = default;
        /// <summary>
        /// 攻撃時の移動速度減衰率
        /// 0 = 減衰しない
        /// 1 = 移動しなくなる
        /// </summary>
        public float MoveSpeedAttenuationRate => this.moveSpeedAttenuationRate;

        [SerializeField]
        private Sprite icon = default;
        public Sprite Icon => this.icon;
    }
}
