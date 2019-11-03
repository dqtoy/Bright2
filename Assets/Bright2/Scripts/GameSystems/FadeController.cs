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

        [SerializeField]
        private FadeImage fadeImage = default;

        [SerializeField]
        private Texture toRightTexture = default;

        [SerializeField]
        private Texture toLeftTexture = default;

        void Awake()
        {
            Broker.Global.Receive<RequestFadeIn>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.fadeImage.UpdateMaskTexture(_this.GetTexture(x.FadeType));
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
                    _this.fadeImage.UpdateMaskTexture(_this.GetTexture(x.FadeType));
                    _this.fade.FadeOut(_this.fadeTimeSeconds, () =>
                    {
                        Broker.Global.Publish(EndFadeOut.Get());
                    });
                    Broker.Global.Publish(BeginFadeOut.Get());
                })
                .AddTo(this);
        }

        private Texture GetTexture(Constants.FadeType type)
        {
            switch(type)
            {
                case Constants.FadeType.ToLeft:
                    return this.toLeftTexture;
                case Constants.FadeType.ToRight:
                    return this.toRightTexture;
                default:
                    Assert.IsTrue(false, $"{type}は未定義です");
                    return null;
            }
        }
    }
}
