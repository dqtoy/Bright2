using HK.Bright2.Database;
using HK.Bright2.WeaponControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

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
