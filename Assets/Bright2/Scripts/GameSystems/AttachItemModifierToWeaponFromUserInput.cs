using System;
using System.Collections.Generic;
using System.Linq;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
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
    public sealed class AttachItemModifierToWeaponFromUserInput : MonoBehaviour
    {
        private Actor actor;

        /// <summary>
        /// アイテム修飾を装着する武器
        /// </summary>
        private InstanceWeapon selectWeapon = null;

        /// <summary>
        /// アイテム修飾で必要な武器のレコード
        /// </summary>
        private List<WeaponRecord> needWeaponRecords = null;

        /// <summary>
        /// アイテム修飾を装着する際に消費する武器
        /// </summary>
        private readonly List<InstanceWeapon> selectConsumeInstanceWeapons = new List<InstanceWeapon>();

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
            this.selectWeapon = null;
            this.needWeaponRecords = null;
            this.selectConsumeInstanceWeapons.Clear();

            this.StartSelectInstanceWeapon(actor);
        }

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

            }));
        }

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

                // 武器の消費が必要なら選択開始
                if (item.NeedItems.IsNeedWeapon)
                {
                    this.needWeaponRecords = item.NeedItems.GetNeedWeaponRecords();
                    Broker.Global.Publish(RequestHideListUI.Get(null));
                    this.StartSelectConsumeInstanceWeapon(actor, this.GetTargetWeaponRecord(), item.ItemModifier);
                }
                else
                {
                    this.Attach(actor, item.ItemModifier);
                }

                // TODO: お金消費


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
                this.StartSelectInstanceWeapon(actor);
            }));
        }

        private void StartSelectConsumeInstanceWeapon(Actor actor, WeaponRecord targetWeaponRecord, ItemModifier itemModifier)
        {
            Assert.IsNotNull(targetWeaponRecord, $"対象となる{typeof(WeaponRecord)}がありません");

            // 所持している武器から対象となる武器を抽出
            var targetInstanceWeapons = actor.StatusController.Inventory.Weapons
                .Where(x => x.WeaponRecord == targetWeaponRecord)
                .ToList();

            Broker.Global.Publish(RequestShowGridUI.Get(targetInstanceWeapons, i =>
            {
                this.selectConsumeInstanceWeapons.Add(targetInstanceWeapons[i]);

                Broker.Global.Publish(RequestHideGridUI.Get());

                var nextTargetWeaponRecord = this.GetTargetWeaponRecord();
                if (nextTargetWeaponRecord == null)
                {
                    this.Attach(actor, itemModifier);

                    if (this.CanAttach)
                    {
                        this.selectConsumeInstanceWeapons.Clear();
                        this.StartSelectItemModifier(actor);
                    }
                    else
                    {
                        this.StartSelectInstanceWeapon(actor);
                    }
                }
                else
                {
                    this.StartSelectConsumeInstanceWeapon(actor, nextTargetWeaponRecord, itemModifier);
                }
            }, () =>
            {

            }));
        }

        private WeaponRecord GetTargetWeaponRecord()
        {
            foreach (var w in this.needWeaponRecords)
            {
                var isTarget = true;
                foreach (var i in this.selectConsumeInstanceWeapons)
                {
                    if (w == i.WeaponRecord)
                    {
                        isTarget = false;
                        break;
                    }
                }

                if(isTarget)
                {
                    return w;
                }
            }

            return null;
        }

        private void Attach(Actor actor, ItemModifier itemModifier)
        {
            actor.StatusController.AttachItemModifierToInstanceWeapon(this.selectWeapon, itemModifier);
            Debug.Log("アタッチ完了");
        }

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
