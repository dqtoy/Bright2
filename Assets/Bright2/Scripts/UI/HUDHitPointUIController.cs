using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// HUDのヒットポイントUIを制御するクラス
    /// </summary>
    public sealed class HUDHitPointUIController : MonoBehaviour
    {
        [SerializeField]
        private Slider slider = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveHitPoint(x.Actor.StatusController);
                })
                .AddTo(this);
        }

        private void ObserveHitPoint(ActorInstanceStatusController statusController)
        {
            Observable.Merge(
                statusController.HitPoint,
                statusController.HitPointMax
            )
            .SubscribeWithState2(this, statusController, (_, _this, s) =>
            {
                _this.UpdateSlider(s.HitPoint.Value, s.HitPointMax.Value);
            })
            .AddTo(this);
        }

        private void UpdateSlider(int hitPoint, int hitPointMax)
        {
            this.slider.value = (float)hitPoint / hitPointMax;
        }
    }
}
