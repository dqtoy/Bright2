using HK.Bright2.GameSystems.Messages.Fade;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// フェードを制御するクラス
    /// </summary>
    public sealed class FadeController : MonoBehaviour
    {
        [SerializeField]
        private Fade fade = default;

        [SerializeField]
        private float fadeTimeSeconds = default;

        void Awake()
        {
            Broker.Global.Receive<RequestFadeIn>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.fade.FadeIn(_this.fadeTimeSeconds, () =>
                    {
                        Broker.Global.Publish(EndFadeIn.Get());
                    });
                    Broker.Global.Publish(BeginFadeIn.Get());
                })
                .AddTo(this);
            
            Broker.Global.Receive<RequestFadeOut>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.fade.FadeOut(_this.fadeTimeSeconds, () =>
                    {
                        Broker.Global.Publish(EndFadeOut.Get());
                    });
                    Broker.Global.Publish(BeginFadeOut.Get());
                })
                .AddTo(this);
        }

    }
}
