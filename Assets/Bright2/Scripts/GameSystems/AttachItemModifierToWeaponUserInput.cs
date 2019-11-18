using System;
using System.Collections.Generic;
using System.Linq;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.ItemControllers;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ユーザー入力によって武器に<see cref="ItemModifier"/>を装着するクラス
    /// </summary>
    public sealed class AttachItemModifierToWeaponUserInput : MonoBehaviour, ISelectConsumeInstanceWeapons
    {
        private Actor actor;

        /// <summary>
        /// アイテム修飾を装着する武器
        /// </summary>
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

        /// <summary>
        /// シーケンスを開始する
        /// </summary>
        private void StartSequence(Actor actor)
        {
            this.selectWeapon = null;

            this.StartSelectInstanceWeapon(actor);
        }

        /// <summary>
        /// アイテム修飾を装着する武器の選択を開始する
        /// </summary>
        private void StartSelectInstanceWeapon(Actor actor)
        {
            Broker.Global.Publish(RequestShowGridUI.Get(actor.StatusController.Inventory.Weapons, i =>
            {
                var instanceWeapon = actor.StatusController.Inventory.Weapons[i];
                if (instanceWeapon.WeaponRecord.ItemModifierLimit <= instanceWeapon.Modifiers.Count)
                {
                    Debug.Log("制限を超えたので装着できない");
                    return;
                }

                this.selectWeapon = instanceWeapon;
                Broker.Global.Publish(RequestHideGridUI.Get());
                this.StartSelectItemModifier(actor);
            }, () =>
            {
                Broker.Global.Publish(RequestHideGridUI.Get());
            }));
        }

        /// <summary>
        /// アイテム修飾の選択を開始する
        /// </summary>
        private void StartSelectItemModifier(Actor actor)
        {
            var items = GameSystem.Instance.MasterData.ItemModifierRecipe.GetViewableRecipes(actor.StatusController.Inventory);
            Broker.Global.Publish(RequestShowListUI.Get(items, i =>
            {
                var inventory = actor.StatusController.Inventory;

                var item = items[i];
                if (!inventory.IsEnough(item.NeedItems))
                {
                    Debug.Log("素材が足りない");
                    return;
                }

                if(!inventory.IsEnough(item.Money))
                {
                    Debug.Log("お金が足りない");
                    return;
                }

                // 武器の消費が必要なら選択開始
                if (item.NeedItems.IsNeedWeapon)
                {
                    Broker.Global.Publish(RequestHideListUI.Get(null));
                    this.StartSelectConsumeInstanceWeapon(actor, item.NeedItems, (selectConsumeInstanceWeapons) =>
                    {
                        this.Attach(actor, item, selectConsumeInstanceWeapons);

                        if (this.CanAttach)
                        {
                            this.StartSelectItemModifier(actor);
                        }
                        else
                        {
                            this.StartSelectInstanceWeapon(actor);
                        }
                    }, () =>
                    {
                        Broker.Global.Publish(RequestHideGridUI.Get());
                        this.StartSelectItemModifier(actor);
                    });
                }
                else
                {
                    this.Attach(actor, item, null);
                }

                // 修飾数が制限値になった場合は終了
                if (!this.CanAttach)
                {
                    Broker.Global.Publish(RequestHideListUI.Get(() =>
                    {
                        this.StartSelectInstanceWeapon(actor);
                    }));
                }
            }, () =>
            {
                Broker.Global.Publish(RequestHideListUI.Get(null));
                this.StartSelectInstanceWeapon(actor);
            }));
        }

        /// <summary>
        /// 選択した武器にアイテム修飾を装着する
        /// </summary>
        private void Attach(Actor actor, ItemModifierRecipeRecord record, List<InstanceWeapon> selectConsumeInstanceWeapons)
        {
            actor.StatusController.Inventory.Consume(record.NeedItems, selectConsumeInstanceWeapons, record.Money);
            actor.StatusController.AttachItemModifierToInstanceWeapon(this.selectWeapon, record.ItemModifier);
            Debug.Log("アタッチ完了");
        }

        /// <summary>
        /// 装着可能か返す
        /// </summary>
        private bool CanAttach
        {
            get
            {
                Assert.IsNotNull(this.selectWeapon);
                return this.selectWeapon.Modifiers.Count < this.selectWeapon.WeaponRecord.ItemModifierLimit;
            }
        }
    }
}
