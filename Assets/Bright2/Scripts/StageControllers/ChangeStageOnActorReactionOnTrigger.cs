using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ChangeStageOnActorReactionOnTrigger : MonoBehaviour, IGameEvent, IActorReactionOnTriggerEnter2D, IActorReactionOnTriggerExit2D
    {
        [SerializeField]
        private Stage prefab = default;

        /// <summary>
        /// ステージ切り替えた後の<see cref="Actor"/>の座標
        /// </summary>
        [SerializeField]
        private Vector2 actorPosition = default;

        [SerializeField]
        private List<string> includeTags = default;

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

            this.ClearGameEvent(actor);
        }

        void IGameEvent.Invoke(Actor invokedActor)
        {
            Broker.Global.Publish(RequestChangeStage.Get(this.prefab));
            invokedActor.Movement.Warp(this.actorPosition);
            
            this.ClearGameEvent(invokedActor);
        }

        private void ClearGameEvent(Actor actor)
        {
            Assert.AreEqual(this, actor.StatusController.GameEvent, $"別の{typeof(IGameEvent)}を解除しようとしました");
            actor.StatusController.SetGameEvent(null);
        }
    }
}
