using HK.Bright2.GameSystems.Messages;
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
    public sealed class TalkUIController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI message = default;

        void Awake()
        {
            Broker.Global.Receive<RequestTalk>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.SetMessage(x.Message);
                })
                .AddTo(this);
        }

        private void SetMessage(string message)
        {
            this.message.text = message;
        }
    }
}
