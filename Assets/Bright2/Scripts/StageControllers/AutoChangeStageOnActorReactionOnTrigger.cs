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
    /// 自動でステージを切り替えるクラス
    /// </summary>
    public sealed class AutoChangeStageOnActorReactionOnTrigger : MonoBehaviour, IActorReactionOnTriggerEnter2D
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

            Broker.Global.Publish(RequestChangeStage.Get(this.prefab));
            actor.Movement.Warp(this.actorPosition);
        }
    }
}
