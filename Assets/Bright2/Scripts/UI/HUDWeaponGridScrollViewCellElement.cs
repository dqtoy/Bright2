using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HUDWeaponGridScrollViewCellElement : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup = default;

        [SerializeField]
        private Image icon = default;

        public void Setup(WeaponRecord weaponRecord)
        {
            this.icon.sprite = weaponRecord.Icon;
            this.canvasGroup.alpha = 1.0f;
        }

        public void Deactive()
        {
            this.icon.sprite = null;
            this.canvasGroup.alpha = 0.0f;
        }
    }
}
