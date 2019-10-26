using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using System.Collections.Generic;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えるギミックデコレーター
    /// </summary>
    public sealed class DamageActor : MonoBehaviour, IActorReactionOnTriggerEnter2D
    {
        [SerializeField]
        private List<string> includeTags = default;
        
        [SerializeField]
        private int damagePower = default;

        [SerializeField]
        private float knockbackPower = default;

        private Actor owner;

        void Awake()
        {
            this.owner = this.GetComponentInParent<Actor>();
            Assert.IsNotNull(this.owner);
        }

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            // オーナーと衝突した場合は何もしない
            if(this.owner == actor)
            {
                return;
            }

            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            actor.StatusController.TakeDamage(this.damagePower);
            actor.Movement.SetGravity(this.GetKnockbackDirection(actor) * this.knockbackPower);
        }

        private Vector2 GetKnockbackDirection(Actor target)
        {
            var result = Vector2.up;
            var diff = target.CachedTransform.position - this.owner.CachedTransform.position;
            result.x = diff.x < 0.0f ? -1.0f : 1.0f;

            return result.normalized;
        }
    }
}
