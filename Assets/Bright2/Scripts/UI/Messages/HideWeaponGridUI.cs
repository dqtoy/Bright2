using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 武器グリッドUIを閉じた際のメッセージ
    /// </summary>
    public sealed class HideWeaponGridUI : Message<HideWeaponGridUI, WeaponGridUIController>
    {
        public WeaponGridUIController Controller => this.param1;
    }
}
