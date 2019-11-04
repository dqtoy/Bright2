using HK.Bright2.Database;
using HK.Bright2.WeaponControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// お金を獲得した際のメッセージ
    /// </summary>
    public sealed class AcquiredMoney : Message<AcquiredMoney, int>
    {
        public int Amount => this.param1;
    }
}
