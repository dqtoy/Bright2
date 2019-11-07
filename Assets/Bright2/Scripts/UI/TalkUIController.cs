using HK.Bright2.GameSystems.Messages;
using HK.Bright2.InputSystems;
using HK.Framework.EventSystems;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// トークUIを制御するクラス
    /// </summary>
    public sealed class TalkUIController : MonoBehaviour, IControllableUserInput
    {
        [SerializeField]
        private CanvasGroup canvasGroup = default;

        [SerializeField]
        private TextMeshProUGUI message = default;

        void Awake()
        {
            this.canvasGroup.alpha = 0.0f;

            Broker.Global.Receive<RequestShowTalk>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.SetMessage(x.Message);
                    Broker.Global.Publish(StartTalk.Get(this));
                })
                .AddTo(this);

            Broker.Global.Receive<RequestHideTalk>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Hidden();
                    Broker.Global.Publish(HideTalk.Get());
                })
                .AddTo(this);
        }

        private void SetMessage(string message)
        {
            this.canvasGroup.alpha = 1.0f;
            this.message.text = message;
        }

        private void Hidden()
        {
            this.canvasGroup.alpha = 0.0f;
        }

        void IControllableUserInput.UpdateInput()
        {
            if(Input.GetButtonDown(InputName.Decide))
            {
                Broker.Global.Publish(EndTalk.Get(this));
            }
        }
    }
}
