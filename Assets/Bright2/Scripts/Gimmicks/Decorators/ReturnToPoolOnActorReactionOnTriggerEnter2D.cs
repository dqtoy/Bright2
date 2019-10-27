using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="Actor"/>と衝突したらギミックをプールするギミックデコレーター
    /// </summary>
    public sealed class ReturnToPoolOnActorReactionOnTriggerEnter2D : MonoBehaviour, IGimmickDecorator, IActorReactionOnTriggerEnter2D
    {
        private Gimmick owner;

        [SerializeField]
        private List<string> includeTags = new List<string>();

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            this.owner.Return();
        }

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.owner = owner;
        }
    }
}
