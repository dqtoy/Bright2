using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="Actor"/>にダメージを与えるギミックデコレーター
    /// </summary>
    public sealed class DamageActor : MonoBehaviour, IGimmickDecorator, IActorReactionOnTriggerEnter2D
    {
        [SerializeField]
        private int damagePower = default;

        private Actor gimmickOwner;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            // オーナーと衝突した場合は何もしない
            if(this.gimmickOwner == actor)
            {
                return;
            }

            actor.StatusController.TakeDamage(this.damagePower);
        }

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.gimmickOwner = gimmickOwner;
        }
    }
}
