using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// 装備中の武器が切り替わった際のメッセージ
    /// </summary>
    public sealed class ChangedEquippedWeapon : Message<ChangedEquippedWeapon, int>
    {
        public int Index => this.param1;
    }
}
