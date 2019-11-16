using HK.Bright2.ItemControllers;
using HK.Framework.EventSystems;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// 武器を獲得した際のメッセージ
    /// </summary>
    public sealed class AcquiredWeapon : Message<AcquiredWeapon, InstanceWeapon>
    {
        public InstanceWeapon Weapon => this.param1;
    }
}
