using UnityEngine;
using UnityEngine.Assertions;
using FancyScrollView;
using System.Collections.Generic;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// リストスクロールビューを制御するクラス
    /// </summary>
    public sealed class ListScrollView : FancyScrollView<ListScrollViewItemData, ListScrollViewContext>
    {
        [SerializeField]
        private Scroller scroller = default;
        public Scroller Scroller => this.scroller;

        [SerializeField]
        private GameObject cellPrefab = default;

        protected override GameObject CellPrefab => this.cellPrefab;

        public float CellInterval => this.cellInterval;

        void Start()
        {
            this.scroller.OnValueChanged(base.UpdatePosition);
        }

        public void UpdateData(IList<ListScrollViewItemData> items)
        {
            this.Context.SelectIndex = 0;
            base.UpdateContents(items);
            this.scroller.SetTotalCount(items.Count);
            this.UpdateCellInterval();
        }

        public void UpdateSelectIndex(int selectIndex)
        {
            this.Context.SelectIndex = selectIndex;
            this.Refresh();
        }

        private void UpdateCellInterval()
        {
            var contentHeight = (this.scroller.transform as RectTransform).rect.height;
            var cellHeight = (this.cellPrefab.transform as RectTransform).rect.height;

            this.cellInterval = (cellHeight / contentHeight) / 1.0f;
        }
    }
}
