using System.Collections.Generic;
using FancyScrollView;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// リストのセルを制御するクラス
    /// </summary>
    public sealed class ListScrollViewCell : FancyScrollViewCell<ListScrollViewItemData, ListScrollViewContext>
    {
        [SerializeField]
        private Image icon = default;
        
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

        public override void UpdateContent(ListScrollViewItemData itemData)
        {
            this.icon.sprite = itemData.Item.Icon;
        }

        public override void UpdatePosition(float position)
        {
            this.transform.localPosition = Vector2.down * (position * this.CachedParentTransform.rect.height);
        }
    }
}
