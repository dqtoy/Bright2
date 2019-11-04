using HK.Bright2.Database;
using HK.Bright2.WeaponControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// アクセサリーを獲得した際のメッセージ
    /// </summary>
    public sealed class AcquiredAccessory : Message<AcquiredAccessory, AccessoryRecord>
    {
        public AccessoryRecord Accessory => this.param1;
    }
}
