using System;
using System.Linq;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.UIControllers.Messages;
using HK.Bright2.WeaponControllers;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ユーザー入力によって武器に<see cref="ItemModifier"/>を装着するクラス
    /// </summary>
    public sealed class AttachItemModifierToWeaponFromUserInput : MonoBehaviour
    {
        private Actor actor;

        private InstanceWeapon selectWeapon = null;

        void Awake()
        {
            Broker.Global.Receive<RequestAttachItemModifierToWeaponFromUserInput>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.StartSequence(x.Actor);
                })
                .AddTo(this);
        }

        private void StartSequence(Actor actor)
        {
            this.StartSelectInstanceWeapon(actor);
        }

        private void StartSelectInstanceWeapon(Actor actor)
        {
            Broker.Global.Publish(RequestShowGridUI.Get(actor.StatusController.Inventory.Weapons));

            Broker.Global.Receive<DecidedGridIndex>()
                .TakeUntil(Broker.Global.Receive<HideGridUI>())
                .SubscribeWithState2(this, actor, (x, _this, _actor) =>
                {
                    var instanceWeapon = _actor.StatusController.Inventory.Weapons[x.Index];
                    if (instanceWeapon.WeaponRecord.ItemModifierLimit <= instanceWeapon.Modifiers.Count)
                    {
                        Debug.Log("制限を超えたので装着できない");
                        return;
                    }

                    _this.selectWeapon = instanceWeapon;
                    Broker.Global.Publish(RequestHideGridUI.Get());
                    _this.StartSelectItemModifier(_actor);
                })
                .AddTo(this);
        }

        private void StartSelectItemModifier(Actor actor)
        {
            var items = GameSystem.Instance.MasterData.ItemModifierRecipe.GetViewableRecipes(actor.StatusController.Inventory);
            Broker.Global.Publish(RequestShowListUI.Get(items));

            Broker.Global.Receive<DecidedListIndex>()
                .TakeUntil(Broker.Global.Receive<HideListUI>())
                .SubscribeWithState3(this, actor, items, (x, _this, _actor, _items) =>
                {
                    var item = _items[x.Index];
                    if (!item.NeedMaterials.IsEnough(_actor.StatusController.Inventory))
                    {
                        Debug.Log("素材が足りない");
                        return;
                    }

                    // TODO: お金消費

                    _actor.StatusController.AttachItemModifierToInstanceWeapon(_this.selectWeapon, item.ItemModifier);
                    Debug.Log("アタッチ完了");

                    // 修飾数が制限値になった場合は終了
                    if(_this.selectWeapon.Modifiers.Count >= _this.selectWeapon.WeaponRecord.ItemModifierLimit)
                    {
                        Broker.Global.Publish(RequestHideListUI.Get());
                    }
                })
                .AddTo(this);

            Broker.Global.Receive<HideListUI>()
                .Take(1)
                .SubscribeWithState2(this, actor, (_, _this, _actor) =>
                {
                    _this.StartSelectInstanceWeapon(_actor);
                })
                .AddTo(this);
        }
    }
}
