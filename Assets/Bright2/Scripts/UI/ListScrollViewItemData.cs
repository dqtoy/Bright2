using System.Collections.Generic;
using HK.Bright2.GameSystems;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// リストのアイテムデータ
    /// </summary>
    public sealed class ListScrollViewItemData
    {
        public IViewableList Item { get; }

        public ListScrollViewItemData(IViewableList item)
        {
            this.Item = item;
        }
    }
}
