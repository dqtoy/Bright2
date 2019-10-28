using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using System.Collections.Generic;
using HK.Bright2.Extensions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="Gimmick"/>が<see cref="Actor"/>にダメージを与えるギミックデコレーター
    /// </summary>
    public sealed class DamageActorFromGimmick : MonoBehaviour, IGiveDamage, IGimmickDecorator, IActorReactionOnTriggerStay2D
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

        private Gimmick owner;

        private Actor gimmickOwner;

        private Collider2D controlledCollider;

        int IGiveDamage.DamagePower => this.damagePower;

        float IGiveDamage.KnockbackPower => this.knockbackPower;

        float IGiveDamage.InfinitySeconds => this.infinitySeconds;

        Actor IGiveDamage.Owner => this.gimmickOwner;

        GameObject IGiveDamage.GiveDamageObject => this.gameObject;

        Collider2D IGiveDamage.GiveDamageCollider => this.controlledCollider;

        List<string> IGiveDamage.IncludeTags => this.includeTags;

        Vector2 IGiveDamage.KnockbackDirection
        {
            get
            {
                var result = Vector2.up;
                result.x = this.owner.Direction == Constants.Direction.Left ? -1.0f : 1.0f;

                return result.normalized;
            }
        }

        void Awake()
        {
            this.controlledCollider = this.GetComponentInChildren<Collider2D>();
            Assert.IsNotNull(this.controlledCollider);
        }

        void IActorReactionOnTriggerStay2D.Do(Actor actor)
        {
            this.GiveDamage(actor);
        }

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.owner = owner;
            this.gimmickOwner = gimmickOwner;
        }
    }
}
