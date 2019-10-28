using System.Collections.Generic;
using FancyScrollView;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HUDWeaponGridScrollViewCell : FancyScrollViewCell<HUDWeaponGridScrollViewItemData>
    {
        [SerializeField]
        private List<HUDWeaponGridScrollViewCellElement> elements = default;
        
        public override void UpdateContent(HUDWeaponGridScrollViewItemData itemData)
        {
            for (var i = 0; i < itemData.Records.Count; i++)
            {
                elements[i].Setup(itemData.Records[i]);
            }
            for (var i = this.elements.Count - itemData.Records.Count; i < this.elements.Count; i++)
            {
                this.elements[i].Deactive();
            }
        }

        public override void UpdatePosition(float position)
        {
        }
    }
}
