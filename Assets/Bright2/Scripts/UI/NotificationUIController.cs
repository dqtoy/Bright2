using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using HK.Framework.Text;
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

        [SerializeField]
        private Sprite moneyIcon = default;

        [SerializeField]
        private StringAsset.Finder moneyFormat = default;

        private Actor owner;

        private NotificationUIElement moneyElement;

        private int acquiredMoney = 0;

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

            this.owner.Broker.Receive<AcquiredMoney>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateMoneyElement(x.Amount);
                })
                .AddTo(this)
                .AddTo(this.owner);
        }

        private NotificationUIElement CreateElement(Sprite sprite, string message)
        {
            var element = this.element.Rent();
            element.Setup(sprite, message);
            element.transform.SetParent(this.viewport, false);
            element.transform.SetAsFirstSibling();

            return element;
        }

        private void CreateMoneyElement(int value)
        {
            if(this.moneyElement == null || !this.moneyElement.IsVisible)
            {
                this.acquiredMoney = value;
                this.moneyElement = this.CreateElement(this.moneyIcon, this.moneyFormat.Format(this.acquiredMoney));
            }
            else
            {
                this.acquiredMoney += value;
                this.moneyElement.Setup(this.moneyIcon, this.moneyFormat.Format(this.acquiredMoney));
            }
        }
    }
}
