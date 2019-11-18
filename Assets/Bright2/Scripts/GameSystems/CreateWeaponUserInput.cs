using System.Collections.Generic;
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
    /// ユーザーの入力によって<see cref="Actor"/>に対して武器を追加するクラス
    /// </summary>
    public sealed class CreateWeaponUserInput : MonoBehaviour, ISelectConsumeInstanceWeapons
    {
        void Awake()
        {
            Broker.Global.Receive<CreateWeaponUserInputMessages.Request>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.StartSequence(x.Actor);
                })
                .AddTo(this);
        }

        private void StartSequence(Actor actor)
        {
            this.StartCreateWeapon(actor);
        }

        private void StartCreateWeapon(Actor actor)
        {
            var items = GameSystem.Instance.MasterData.WeaponRecipe.GetViewableRecipes(actor.StatusController.Inventory);
            Broker.Global.Publish(RequestShowGridUI.Get(items, i =>
            {
                var item = items[i];

                if(!actor.StatusController.Inventory.IsEnough(item.NeedItems))
                {
                    Debug.Log("素材が足りない");
                    return;
                }

                if(!actor.StatusController.Inventory.IsEnough(item.Money))
                {
                    Debug.Log("お金が足りない");
                    return;
                }

                if (item.NeedItems.IsNeedWeapon)
                {
                    this.StartSelectConsumeInstanceWeapon(actor, item.NeedItems, selectConsumeInstanceWeapons =>
                    {
                        Broker.Global.Publish(RequestHideGridUI.Get());
                        this.AddWeapon(actor, item, selectConsumeInstanceWeapons);
                        this.StartCreateWeapon(actor);
                    }, () =>
                    {
                        Broker.Global.Publish(RequestHideGridUI.Get());
                        this.StartCreateWeapon(actor);
                    });
                }
                else
                {
                    this.AddWeapon(actor, item, null);
                }
            }, () =>
            {
                Broker.Global.Publish(RequestHideGridUI.Get());
                Broker.Global.Publish(CreateWeaponUserInputMessages.End.Get());
            }));
        }

        private void AddWeapon(Actor actor, WeaponRecipeRecord record, List<InstanceWeapon> selectConsumeInstanceWeapons)
        {
            actor.StatusController.Inventory.Consume(record.NeedItems, selectConsumeInstanceWeapons, record.Money);
            actor.StatusController.AddWeapon(record.WeaponRecord);
            Debug.Log("作成完了");
        }
    }
}
