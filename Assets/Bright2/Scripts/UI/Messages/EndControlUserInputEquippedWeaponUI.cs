using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// <see cref="EquippedWeaponUIController"/>がユーザー入力を終了する際のメッセージ
    /// </summary>
    public sealed class EndControlUserInputEquippedWeaponUI : Message<EndControlUserInputEquippedWeaponUI, EquippedWeaponUIController>
    {
        public EquippedWeaponUIController Controller => this.param1;
    }
}
