using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// <see cref="Actor"/>の重力を設定するクラス
    /// </summary>
    public sealed class SetGravityOnActorReactionOnTrigger : MonoBehaviour, IActorReactionOnTriggerEnter2D
    {
        [SerializeField]
        private Vector2 gravity = default;

        [SerializeField]
        private List<string> includeTags = default;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            actor.Movement.SetGravity(this.gravity);
        }
    }
}
