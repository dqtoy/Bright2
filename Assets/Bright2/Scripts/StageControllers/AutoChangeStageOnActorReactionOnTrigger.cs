using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages.Fade;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UniRx.Triggers;
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

        [SerializeField]
        private Constants.Direction direction = default;

        [SerializeField]
        private float fadeTimeSeconds = default;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Broker.Global.Receive<EndFadeIn>()
                .Take(1)
                .SubscribeWithState2(this, actor, (_, _this, _actor) =>
                {
                    Broker.Global.Publish(RequestChangeStage.Get(_this.prefab));
                    _actor.Movement.Warp(_this.actorPosition);
                    Broker.Global.Publish(RequestFadeOut.Get(_this.direction.ToReverse().ToFadeType(), _this.fadeTimeSeconds));
                    Broker.Global.Publish(EndChangeStage.Get());
                });

            Broker.Global.Publish(BeginChangeStage.Get());
            Broker.Global.Publish(RequestFadeIn.Get(this.direction.ToFadeType(), this.fadeTimeSeconds));

            actor.UpdateAsObservable()
                .TakeUntil(Broker.Global.Receive<EndChangeStage>())
                .SubscribeWithState2(this, actor, (_, _this, _actor) =>
                {
                    _actor.Broker.Publish(RequestMove.Get(_this.direction.ToVector2()));
                })
                .AddTo(actor);
        }
    }
}
