using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects;
using HK.Bright2.GimmickControllers;
using HK.Bright2.ItemControllers;
using UnityEngine;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えるコンポーネントの抽象クラス
    /// /// </summary>
    public abstract class GiveDamageActorComponent : MonoBehaviour, IGiveDamage, IAffectedSpeedUp
    {
        [SerializeField]
        private int damagePower = default;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float criticalRate = default;

        [SerializeField]
        private float knockbackPower = default;

        /// <summary>
        /// 攻撃が当たった際に相手に付与する無敵時間
        /// </summary>
        [SerializeField]
        private float infinitySeconds = default;

        [SerializeField]
        private Constants.WeaponType weaponType = default;

        [SerializeField]
        private List<string> includeTags = default;

        [SerializeField]
        private Collider2D controlledCollider = default;

        /// <summary>
        /// 攻撃が貫通する回数
        /// </summary>
        [SerializeField]
        protected int penetrationCount = default;

        [SerializeField]
        private List<GiveDamageActorAdditionalEffect> additionalEffects = default;

        protected int currentPenetrationCount;

        int IGiveDamage.DamagePower => this.damagePower;

        float IGiveDamage.CriticalRate => this.criticalRate;

        float IGiveDamage.KnockbackPower => this.knockbackPower;

        float IGiveDamage.InfinitySeconds => Calculator.GetFireSpeedUp(this.infinitySeconds, this.fireSpeedUpRate);

        Constants.WeaponType IGiveDamage.WeaponType => this.weaponType;

        GameObject IGiveDamage.GiveDamageObject => this.gameObject;

        Collider2D IGiveDamage.GiveDamageCollider => this.controlledCollider;

        List<string> IGiveDamage.IncludeTags => this.includeTags;

        int IGiveDamage.CurrentPenetrationCount
        {
            get
            {
                return this.currentPenetrationCount;
            }
            set
            {
                this.currentPenetrationCount = value;
            }
        }

        List<GiveDamageActorAdditionalEffect> IGiveDamage.AdditionalEffects => this.additionalEffects;

        public abstract Actor Owner { get; }

        public abstract Actor Damager { get; }

        public abstract GameObject Root { get; }

        public abstract Vector2 KnockbackDirection { get; }

        InstanceWeapon IGiveDamage.InstanceWeapon { get; set; }

        private float fireSpeedUpRate;

        void IAffectedSpeedUp.Affected(float rate)
        {
            this.fireSpeedUpRate = rate;
        }
    }
}
