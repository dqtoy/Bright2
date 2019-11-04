using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 通知UIを制御するクラス
    /// </summary>
    public sealed class NotificationUIController : MonoBehaviour
    {
        [SerializeField]
        private NotificationUIElement element = default;

        [SerializeField]
        private Transform viewport = default;

        private Actor owner;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveActor(x.Actor);
                })
                .AddTo(this);
        }

        private void ObserveActor(Actor actor)
        {
            this.owner = actor;

            this.owner.Broker.Receive<AcquiredWeapon>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateElement(x.Weapon.WeaponRecord.Icon, x.Weapon.WeaponRecord.WeaponName);
                })
                .AddTo(this)
                .AddTo(this.owner);

            this.owner.Broker.Receive<AcquiredAccessory>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateElement(x.Accessory.Icon, x.Accessory.AccessoryName);
                })
                .AddTo(this)
                .AddTo(this.owner);
        }

        private void CreateElement(Sprite sprite, string message)
        {
            var element = this.element.Rent();
            element.Setup(sprite, message);
            element.transform.SetParent(this.viewport, false);
            element.transform.SetAsFirstSibling();
        }
    }
}
