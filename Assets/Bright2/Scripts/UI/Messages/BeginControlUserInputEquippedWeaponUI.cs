using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// <see cref="EquippedWeaponUIController"/>がユーザー入力を開始する際のメッセージ
    /// </summary>
    public sealed class BeginControlUserInputEquippedWeaponUI : Message<BeginControlUserInputEquippedWeaponUI, EquippedWeaponUIController>
    {
        public EquippedWeaponUIController Controller => this.param1;
    }
}
