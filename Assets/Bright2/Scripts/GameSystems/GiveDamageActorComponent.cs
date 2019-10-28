using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えるコンポーネントの抽象クラス
    /// /// </summary>
    public abstract class GiveDamageActorComponent : MonoBehaviour, IGiveDamage
    {
        [SerializeField]
        private int damagePower = default;

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

        int IGiveDamage.DamagePower => this.damagePower;

        float IGiveDamage.KnockbackPower => this.knockbackPower;

        float IGiveDamage.InfinitySeconds => this.infinitySeconds;

        GameObject IGiveDamage.GiveDamageObject => this.gameObject;

        Collider2D IGiveDamage.GiveDamageCollider => this.controlledCollider;

        List<string> IGiveDamage.IncludeTags => this.includeTags;
        
        public abstract Actor Owner { get; }

        public abstract Vector2 KnockbackDirection { get; }
    }
}
