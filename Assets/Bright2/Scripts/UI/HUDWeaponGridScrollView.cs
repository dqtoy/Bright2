using UnityEngine;
using UnityEngine.Assertions;
using FancyScrollView;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HUDWeaponGridScrollView : FancyScrollView.FancyScrollView<HUDWeaponGridScrollViewItemData>
    {
        [SerializeField]
        private GameObject cellPrefab = default;

        protected override GameObject CellPrefab => this.cellPrefab;
    }
}
