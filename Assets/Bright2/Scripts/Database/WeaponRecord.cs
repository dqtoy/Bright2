using System;
using HK.Bright2.GameSystems;
using HK.Bright2.GimmickControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 武器マスターデータのレコード
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Weapon/Record")]
    public sealed class WeaponRecord : MasterDataRecord, IMasterDataRecordId, IIconHolder
    {
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder weaponName = default;
        public string WeaponName => this.weaponName.Get;

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

        /// <summary>
        /// 長押しによる自動攻撃が可能か
        /// </summary>
        [SerializeField]
        private bool canAutoAttack = default;
        public bool CanAutoAttack => this.canAutoAttack;

        [SerializeField]
        private Sprite icon = default;
        public Sprite Icon => this.icon;

        /// <summary>
        /// アイテム修飾をアタッチ出来る制限数
        /// </summary>
        [SerializeField]
        private int itemModifierLimit = default;
        public int ItemModifierLimit => this.itemModifierLimit;
    }
}
