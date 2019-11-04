using System.Collections.Generic;
using HK.Bright2.GameSystems;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GridScrollViewItemData
    {
        public List<IIconHolder> Records { get; }

        public int VerticalIndex { get; }

        public GridScrollViewItemData(int verticalIndex)
        {
            this.Records = new List<IIconHolder>();
            this.VerticalIndex = verticalIndex;
        }

        public bool CanAddRecord => this.Records.Count < GridScrollViewCell.ElementMax;
    }
}
