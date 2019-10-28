using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using System.Collections.Generic;
using HK.Bright2.GameSystems;
using HK.Bright2.Extensions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えるギミックデコレーター
    /// </summary>
    public sealed class DamageActor : MonoBehaviour, IGiveDamage, IActorReactionOnTriggerEnter2D
    {
        [SerializeField]
        private int damagePower = default;

        [SerializeField]
        private float knockbackPower = default;

        [SerializeField]
        private float infinitySeconds = default;

        [SerializeField]
        private List<string> includeTags = default;

        private Collider2D controlledCollider;

        private Actor owner;

        private Actor target;

        int IGiveDamage.DamagePower => this.damagePower;

        float IGiveDamage.KnockbackPower => this.knockbackPower;

        float IGiveDamage.InfinitySeconds => this.infinitySeconds;

        Actor IGiveDamage.Owner => this.owner;

        GameObject IGiveDamage.GiveDamageObject => this.gameObject;

        Collider2D IGiveDamage.GiveDamageCollider => this.controlledCollider;

        List<string> IGiveDamage.IncludeTags => this.includeTags;

        Vector2 IGiveDamage.KnockbackDirection
        {
            get
            {
                Assert.IsNotNull(this.target);
                var result = Vector2.up;
                var diff = this.target.CachedTransform.position - this.owner.CachedTransform.position;
                result.x = diff.x < 0.0f ? -1.0f : 1.0f;

                return result.normalized;
            }
        }

        void Awake()
        {
            this.owner = this.GetComponentInParent<Actor>();
            Assert.IsNotNull(this.owner);

            this.controlledCollider = this.GetComponentInChildren<Collider2D>();
            Assert.IsNotNull(this.controlledCollider);
        }

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            this.target = actor;
            this.GiveDamage(actor);
        }
    }
}
