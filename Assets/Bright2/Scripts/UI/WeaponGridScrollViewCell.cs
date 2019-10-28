using System.Collections.Generic;
using FancyScrollView;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WeaponGridScrollViewCell : FancyScrollViewCell<WeaponGridScrollViewItemData, WeaponGridScrollViewContext>
    {
        [SerializeField]
        private List<WeaponGridScrollViewCellElement> elements = default;

        private RectTransform cachedParentTransform;
        public RectTransform CachedParentTransform
        {
            get
            {
                if(this.cachedParentTransform == null)
                {
                    this.cachedParentTransform = this.transform.parent as RectTransform;
                }

                return this.cachedParentTransform;
            }
        }

        public const int ElementMax = 6;

        public override void UpdateContent(WeaponGridScrollViewItemData itemData)
        {
            for (var i = 0; i < itemData.Records.Count; i++)
            {
                var isSelect = ((itemData.VerticalIndex * WeaponGridScrollViewCell.ElementMax) + i) == this.Context.SelectIndex;
                elements[i].Setup(itemData.Records[i], isSelect);
            }
            for (var i = this.elements.Count - (this.elements.Count - itemData.Records.Count); i < this.elements.Count; i++)
            {
                this.elements[i].Deactive();
            }
        }

        public override void UpdatePosition(float position)
        {
            this.transform.localPosition = Vector2.down * (position * this.CachedParentTransform.rect.height);
        }
    }
}
