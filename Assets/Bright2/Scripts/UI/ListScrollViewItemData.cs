using System.Collections.Generic;
using HK.Bright2.GameSystems;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// リストのアイテムデータ
    /// </summary>
    public sealed class ListScrollViewItemData
    {
        public int Index { get; }
        public IViewableList Item { get; }

        public ListScrollViewItemData(int index, IViewableList item)
        {
            this.Index = index;
            this.Item = item;
        }
    }
}
