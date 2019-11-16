using HK.Bright2.Database;
using HK.Framework.EventSystems;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// 素材を獲得した際のメッセージ
    /// </summary>
    public sealed class AcquiredMaterial : Message<AcquiredMaterial, MaterialRecord>
    {
        public MaterialRecord MaterialRecord => this.param1;
    }
}
