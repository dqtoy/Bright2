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

        /// <summary>
        /// リストに表示されている先頭のインデックス
        /// </summary>
        private int headIndex = 0;

        private int index = 0;

        private IReadOnlyList<IViewableList> items;

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

        private List<ListScrollViewItemData> CreateItems(IReadOnlyList<IViewableList> items)
        {
            this.items = items;
            var result = new List<ListScrollViewItemData>();

            if(items == null)
            {
                return result;
            }

            for (var i = 0; i < items.Count; i++)
            {
                result.Add(new ListScrollViewItemData(i, items[i]));
            }

            return result;
        }

        void IControllableUserInput.UpdateInput()
        {
            if (Input.GetButtonDown(InputName.Left))
            {
                this.SubIndex((int)(1 / this.scrollView.CellInterval));
            }
            if (Input.GetButtonDown(InputName.Right))
            {
                this.AddIndex((int)(1 / this.scrollView.CellInterval));
            }
            if (Input.GetButtonDown(InputName.Up))
            {
                this.SubIndex(1);
            }
            if (Input.GetButtonDown(InputName.Down))
            {
                this.AddIndex(1);
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

        private void SubIndex(int value)
        {
            this.index = Mathf.Max(this.index - value, 0);
            this.scrollView.UpdateSelectIndex(this.index);

            if (this.headIndex > this.index)
            {
                this.headIndex = this.index;
                this.scrollView.Scroller.JumpTo(this.headIndex);
            }
        }

        private void AddIndex(int value)
        {
            this.index = Mathf.Min(this.index + value, this.items.Count - 1);
            this.scrollView.UpdateSelectIndex(this.index);

            var diffIndex = this.index - this.headIndex;
            if (diffIndex * this.scrollView.CellInterval > 1.0f)
            {
                this.headIndex += value;
                this.scrollView.Scroller.JumpTo(this.headIndex);
            }
        }
   }
}
