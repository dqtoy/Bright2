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
    public sealed class GiveDamageActorFromGimmick : GiveDamageActorComponent, IGimmickDecorator, IActorReactionOnTriggerStay2D
    {
        private Gimmick owner;

        private Actor gimmickOwner;

        private Actor target;

        public override Actor Owner => this.gimmickOwner;

        public override Actor Damager => this.target;

        public override GameObject Root => this.owner.gameObject;

        public override Vector2 KnockbackDirection
        {
            get
            {
                var result = Vector2.up;
                result.x = this.owner.Direction == Constants.Direction.Left ? -1.0f : 1.0f;

                return result.normalized;
            }
        }

        void IActorReactionOnTriggerStay2D.Do(Actor actor)
        {
            this.target = actor;
            this.GiveDamage(actor);
        }

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.owner = owner;
            this.gimmickOwner = gimmickOwner;
            this.currentPenetrationCount = this.penetrationCount;
        }
    }
}
