using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using System.Collections.Generic;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.SequenceGameEvents;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// <see cref="SequenceGameEvent"/>を<see cref="Actor.StatusController.GameEvent"/>に登録するクラス
    /// </summary>
    public sealed class InvokeSequenceGameEventOnActorReactionOnTrigger : MonoBehaviour, IActorReactionOnTriggerEnter2D, IActorReactionOnTriggerExit2D
    {
        [SerializeField]
        private SequenceGameEvent sequenceGameEvent = default;
        
        [SerializeField]
        private List<string> includeTags = default;

        void Awake()
        {
            Assert.IsNotNull(this.sequenceGameEvent, $"{this.name}に{typeof(SequenceGameEvent)}が設定されていません");
        }
        
        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            actor.StatusController.SetGameEvent(this.sequenceGameEvent);
        }

        void IActorReactionOnTriggerExit2D.Do(Actor actor)
        {
            if (!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.AreEqual(this.sequenceGameEvent, actor.StatusController.GameEvent);
            actor.StatusController.SetGameEvent(null);
        }
    }
}
