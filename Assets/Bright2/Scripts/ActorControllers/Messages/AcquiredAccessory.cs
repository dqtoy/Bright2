using HK.Bright2.Database;
using HK.Framework.EventSystems;

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
