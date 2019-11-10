using System.Collections.Generic;
using HK.Bright2.GameSystems;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// リストのアイテムデータ
    /// </summary>
    public sealed class ListScrollViewItemData
    {
        public IIconHolder Item { get; }

        public ListScrollViewItemData(IIconHolder item)
        {
            this.Item = item;
        }
    }
}
