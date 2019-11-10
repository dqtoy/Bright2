using System.Collections.Generic;
using System.Linq;
using HK.Bright2.GameSystems;
using HK.Bright2.InputSystems;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// リストUIを制御するクラス
    /// </summary>
    public sealed class ListUIController : MonoBehaviour, IControllableUserInput
    {
        [SerializeField]
        private CanvasGroup canvasGroup = default;

        [SerializeField]
        private ListScrollView scrollView = default;

        private int index = 0;

        private IReadOnlyList<IIconHolder> items;

        void Awake()
        {
            this.canvasGroup.alpha = 0.0f;

            Broker.Global.Receive<RequestShowListUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.index = 0;
                    _this.canvasGroup.alpha = 1.0f;
                    _this.scrollView.UpdateData(_this.CreateItems(x.Items.ToList()));
                    Broker.Global.Publish(ShowListUI.Get(_this));
                })
                .AddTo(this);

            Broker.Global.Receive<RequestHideGridUI>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    Broker.Global.Publish(HideListUI.Get(_this));
                })
                .AddTo(this);

            Broker.Global.Receive<HideGridUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.canvasGroup.alpha = 0.0f;
                })
                .AddTo(this);
        }

        private List<ListScrollViewItemData> CreateItems(IReadOnlyList<IIconHolder> icons)
        {
            this.items = icons;
            var result = new List<ListScrollViewItemData>();

            if(icons == null)
            {
                return result;
            }

            foreach(var i in icons)
            {
                result.Add(new ListScrollViewItemData(i));
            }

            return result;
        }

        void IControllableUserInput.UpdateInput()
        {
            if (Input.GetButtonDown(InputName.Left))
            {
                this.index--;
                this.scrollView.UpdateSelectIndex(this.index);
            }
            if (Input.GetButtonDown(InputName.Right))
            {
                this.index++;
                this.scrollView.UpdateSelectIndex(this.index);
            }
            if (Input.GetButtonDown(InputName.Up))
            {
                this.index--;
                this.scrollView.UpdateSelectIndex(this.index);
            }
            if (Input.GetButtonDown(InputName.Down))
            {
                this.index++;
                this.scrollView.UpdateSelectIndex(this.index);
            }
            if(Input.GetButtonDown(InputName.Decide))
            {
                Broker.Global.Publish(DecidedListIndex.Get(this.index));
            }
            if(Input.GetButtonDown(InputName.Cancel))
            {
                Broker.Global.Publish(HideListUI.Get(this));
            }
        }
   }
}
