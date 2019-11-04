using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class OpenTreasureOnActorReactionOnTrigger : MonoBehaviour, IGameEvent, IActorReactionOnTriggerEnter2D, IActorReactionOnTriggerExit2D
    {
        [SerializeField]
        private List<string> includeTags = default;

        [SerializeField]
        private Vector3 offset = default;

        [SerializeField]
        private List<WeaponRecord> weapons = default;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            actor.StatusController.SetGameEvent(this);
        }

        void IActorReactionOnTriggerExit2D.Do(Actor actor)
        {
            if (!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.AreEqual(actor.StatusController.GameEvent, this);
            actor.StatusController.SetGameEvent(null);
        }

        void IGameEvent.Invoke(Actor invokedActor)
        {
            foreach(var w in this.weapons)
            {
                Broker.Global.Publish(RequestSpawnWeapon.Get(invokedActor, w, this.transform.position + this.offset));
            }

            Destroy(this.gameObject);
        }
    }
}
