using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 装備中の武器のインデックスを選択した際のメッセージ
    /// </summary>
    public sealed class SelectEquippedWeaponIndex : Message<SelectEquippedWeaponIndex, int>
    {
        public int Index => this.param1;
    }
}
