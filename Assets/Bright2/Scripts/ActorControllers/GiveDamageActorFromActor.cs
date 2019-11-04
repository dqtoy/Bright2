using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using System.Collections.Generic;
using HK.Bright2.GameSystems;
using HK.Bright2.Extensions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>が<see cref="Actor"/>に対してダメージを与えるクラス
    /// </summary>
    public sealed class GiveDamageActorFromActor : GiveDamageActorComponent, IActorReactionOnTriggerEnter2D
    {
        private Actor owner;

        private Actor target;

        public override Actor Owner => this.owner;

        public override Actor Damager => this.target;

        public override GameObject Root => this.owner.gameObject;

        public override Vector2 KnockbackDirection
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

            this.currentPenetrationCount = this.penetrationCount;
        }

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            this.target = actor;
            this.GiveDamage(actor);
        }
    }
}
