using UnityEngine;
using UnityEngine.Assertions;
using FancyScrollView;
using System.Collections.Generic;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WeaponGridScrollView : FancyScrollView.FancyScrollView<WeaponGridScrollViewItemData>
    {
        [SerializeField]
        private Scroller scroller = default;

        [SerializeField]
        private GameObject cellPrefab = default;

        protected override GameObject CellPrefab => this.cellPrefab;

        public void UpdateData(IList<WeaponGridScrollViewItemData> items)
        {
            base.UpdateContents(items);
            this.scroller.SetTotalCount(items.Count);
            this.UpdateCellInterval();
        }

        private void UpdateCellInterval()
        {
            var contentHeight = (this.scroller.transform as RectTransform).rect.height;
            var cellHeight = (this.cellPrefab.transform as RectTransform).rect.height;

            this.cellInterval = (cellHeight / contentHeight) / 1.0f;
        }
    }
}
