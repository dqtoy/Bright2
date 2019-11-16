using HK.Framework.EventSystems;

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
