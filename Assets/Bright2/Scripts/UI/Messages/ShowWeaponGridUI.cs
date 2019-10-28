using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 武器グリッドUIを表示した際のメッセージ
    /// </summary>
    public sealed class ShowWeaponGridUI : Message<ShowWeaponGridUI, WeaponGridUIController>
    {
        public WeaponGridUIController Controller => this.param1;
    }
}
