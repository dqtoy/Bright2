using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えるギミックデコレーター
    /// </summary>
    public sealed class DamageActorGimmick : MonoBehaviour, IGimmickDecorator, IActorReactionOnTriggerEnter2D
    {
        [SerializeField]
        private int damagePower = default;

        [SerializeField]
        private float knockbackPower = default;

        private Gimmick owner;

        private Actor gimmickOwner;

        private Collider2D controlledCollider;

        void Awake()
        {
            this.controlledCollider = this.GetComponentInChildren<Collider2D>();
        }

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            // オーナーと衝突した場合は何もしない
            if(this.gimmickOwner == actor)
            {
                return;
            }

            var generationSource = this.controlledCollider.ClosestPoint(actor.CachedTransform.position);

            actor.StatusController.TakeDamage(this.damagePower, generationSource);
            actor.Movement.SetGravity(this.GetKnockbackDirection() * this.knockbackPower);
        }

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.owner = owner;
            this.gimmickOwner = gimmickOwner;
        }

        private Vector2 GetKnockbackDirection()
        {
            var result = Vector2.up;
            result.x = this.owner.Direction == Constants.Direction.Left ? -1.0f : 1.0f;

            return result.normalized;
        }
    }
}
