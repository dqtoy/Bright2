using UnityEngine;
using UnityEngine.Assertions;
using FancyScrollView;
using System.Collections.Generic;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GridScrollView : FancyScrollView<GridScrollViewItemData, GridScrollViewContext>
    {
        [SerializeField]
        private Scroller scroller = default;

        [SerializeField]
        private GameObject cellPrefab = default;

        protected override GameObject CellPrefab => this.cellPrefab;

        public void UpdateData(IList<GridScrollViewItemData> items)
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
