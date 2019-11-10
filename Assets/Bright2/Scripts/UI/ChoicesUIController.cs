using System.Collections.Generic;
using HK.Bright2.InputSystems;
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
    public sealed class ChoicesUIController : MonoBehaviour, IControllableUserInput
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
                this.elements.Add(element);
            }

            this.canvasGroup.alpha = 1.0f;
            Broker.Global.Publish(ShowChoicesUI.Get(this));
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

        private void AddIndex(int value)
        {
            this.currentIndex = (int)Mathf.Repeat(this.currentIndex + value, this.elements.Count);
        }

        private void SetColor()
        {
            for (var i = 0; i < this.elements.Count; i++)
            {
                var selected = this.currentIndex == i;
                this.elements[i].SetColor(selected);
            }
        }

        void IControllableUserInput.UpdateInput()
        {
            if (Input.GetButtonDown(InputName.Up))
            {
                this.AddIndex(1);
                this.SetColor();
            }
            if (Input.GetButtonDown(InputName.Down))
            {
                this.AddIndex(-1);
                this.SetColor();
            }
            if(Input.GetButtonDown(InputName.Decide))
            {
                this.Hide();
                Broker.Global.Publish(HideChoicesUI.Get(this));
                Broker.Global.Publish(DecideChoicesIndex.Get(this.currentIndex));
            }
        }
    }
}
