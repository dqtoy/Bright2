using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えるギミックデコレーター
    /// </summary>
    public sealed class DamageActorGimmick : MonoBehaviour, IGimmickDecorator, IActorReactionOnTriggerStay2D
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

        private Gimmick owner;

        private Actor gimmickOwner;

        private Collider2D controlledCollider;

        void Awake()
        {
            this.controlledCollider = this.GetComponentInChildren<Collider2D>();
        }

        void IActorReactionOnTriggerStay2D.Do(Actor actor)
        {
            // オーナーと衝突した場合は何もしない
            if (this.gimmickOwner == actor)
            {
                return;
            }

            if(actor.StatusController.IsInfinity(this.gameObject))
            {
                return;
            }

            var generationSource = this.controlledCollider.ClosestPoint(actor.CachedTransform.position);

            actor.StatusController.TakeDamage(this.damagePower, generationSource);
            actor.Movement.SetGravity(this.GetKnockbackDirection() * this.knockbackPower);
            actor.StatusController.AddInfinityStatus(this.gameObject, this.infinitySeconds);
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
