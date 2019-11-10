using System.Collections.Generic;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 選択肢UIを制御するクラス
    /// </summary>
    public sealed class ChoicesUIController : MonoBehaviour
    {
        [SerializeField]
        private ChoicesUIElement elementPrefab = default;

        [SerializeField]
        private Transform elementParent = default;

        [SerializeField]
        private CanvasGroup canvasGroup = default;

        private int currentIndex = 0;

        private readonly List<ChoicesUIElement> elements = new List<ChoicesUIElement>();

        void Awake()
        {
            Broker.Global.Receive<RequestShowChoicesUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.currentIndex = 0;
                    _this.Setup(x.Messages);
                })
                .AddTo(this);

            this.Hide();
        }

        private void Setup(string[] messages)
        {
            for (var i = 0; i < messages.Length; i++)
            {
                var message = messages[i];
                var element = this.elementPrefab.Rent();
                element.transform.SetParent(this.elementParent, false);
                element.transform.SetAsLastSibling();
                element.Setup(message);
                element.SetColor(this.currentIndex == i);
            }

            this.canvasGroup.alpha = 1.0f;
        }

        private void Hide()
        {
            this.canvasGroup.alpha = 0.0f;
            foreach(var e in this.elements)
            {
                e.Return();
            }

            this.elements.Clear();
        }
    }
}
