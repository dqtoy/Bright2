using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// HUDの酸素UIを制御するクラス
    /// </summary>
    public sealed class HUDOxygenUIController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup rootCanvasGroup = default;

        [SerializeField]
        private Slider slider = default;

        private float currentTimer = 0.0f;

        void Awake()
        {
            this.rootCanvasGroup.alpha = 0.0f;

            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveEnterUnderWater(x.Actor);
                    _this.ObserveExitUnderWater(x.Actor);
                })
                .AddTo(this);
        }

        private void ObserveEnterUnderWater(Actor actor)
        {
            actor.Broker.Receive<EnterUnderWater>()
                .SubscribeWithState2(this, actor, (_, _this, _actor) =>
                {
                    _this.rootCanvasGroup.alpha = 1.0f;
                    _this.RegisterUpdate(_actor);
                })
                .AddTo(actor);
        }

        private void ObserveExitUnderWater(Actor actor)
        {
            actor.Broker.Receive<ExitUnderWater>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.rootCanvasGroup.alpha = 0.0f;
                    _this.currentTimer = 0.0f;
                    _this.UpdateSlider();
                })
                .AddTo(actor);
        }

        private void RegisterUpdate(Actor actor)
        {
            actor.UpdateAsObservable()
                .TakeUntil(actor.Broker.Receive<ExitUnderWater>())
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.currentTimer += Time.deltaTime;
                    _this.UpdateSlider();
                });
        }

        private void UpdateSlider()
        {
            this.slider.value = (Constants.LackOfOxygenSeconds - this.currentTimer) / Constants.LackOfOxygenSeconds;
        }
    }
}
