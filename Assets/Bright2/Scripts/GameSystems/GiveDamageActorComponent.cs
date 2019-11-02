using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects;
using HK.Bright2.GimmickControllers;
using UnityEngine;
using UnityEngine.Assertions;

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
        private List<string> includeTags = default;

        [SerializeField]
        private Collider2D controlledCollider = default;

        [SerializeField]
        private List<GiveDamageActorAdditionalEffect> additionalEffects = default;

        int IGiveDamage.DamagePower => this.damagePower;

        float IGiveDamage.CriticalRate => this.criticalRate;

        float IGiveDamage.KnockbackPower => this.knockbackPower;

        float IGiveDamage.InfinitySeconds => Calculator.GetFireSpeedUp(this.infinitySeconds, this.fireSpeedUpRate);

        GameObject IGiveDamage.GiveDamageObject => this.gameObject;

        Collider2D IGiveDamage.GiveDamageCollider => this.controlledCollider;

        List<string> IGiveDamage.IncludeTags => this.includeTags;

        List<GiveDamageActorAdditionalEffect> IGiveDamage.AdditionalEffects => this.additionalEffects;

        public abstract Actor Owner { get; }

        public abstract Actor Damager { get; }

        public abstract GameObject Root { get; }

        public abstract Vector2 KnockbackDirection { get; }

        private float fireSpeedUpRate;

        void IAffectedSpeedUp.Affected(float rate)
        {
            this.fireSpeedUpRate = rate;
        }
    }
}
