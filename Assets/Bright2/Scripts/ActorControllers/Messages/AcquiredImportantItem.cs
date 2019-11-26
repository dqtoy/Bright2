using HK.Bright2.Database;
using HK.Framework.EventSystems;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// 大事なアイテムを獲得した際のメッセージ
    /// </summary>
    public sealed class AcquiredImportantItem : Message<AcquiredImportantItem, ImportantItemRecord>
    {
        public ImportantItemRecord ImportantItemRecord => this.param1;
    }
}
