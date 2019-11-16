using System;
using System.Linq;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.UIControllers;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ユーザーの入力によって<see cref="Actor"/>の装備中のアクセサリーを切り替えるシーケンスを制御するクラス
    /// </summary>
    public sealed class ChangeAccessorySequenceFromUserInput : MonoBehaviour
    {
        private int possessionAccessoryIndex;

        private IDisposable publishEndMessage = null;

        void Awake()
        {
            Broker.Global.Receive<RequestChangeAccessorySequenceFromUserInput>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.StartSequence(x.Actor);
                })
                .AddTo(this);
        }

        private void StartSequence(Actor actor)
        {
            this.StartSelectPossessionAccessoryIndex(actor);
        }

        /// <summary>
        /// 所持しているアクセサリーのインデックス選択を開始する
        /// </summary>
        private void StartSelectPossessionAccessoryIndex(Actor actor)
        {
            var items = actor.StatusController.Inventory.Accessories;
            Broker.Global.Publish(RequestShowGridUI.Get(items, i =>
            {
                this.possessionAccessoryIndex = i;

                Broker.Global.Publish(RequestHideGridUI.Get());
                this.StartChangeEquippedAccessory(actor);
            }, () =>
            {
                Broker.Global.Publish(EndChangeAccessorySequenceFromUserInput.Get());
            }));
        }

        private void StartChangeEquippedAccessory(Actor actor)
        {
            var items = actor.StatusController.EquippedAccessoryIcons;
            Broker.Global.Publish(RequestShowGridUI.Get(items, i =>
            {
                actor.StatusController.ChangeEquippedAccessory(i, this.possessionAccessoryIndex);
                Broker.Global.Publish(RequestHideGridUI.Get());
            }, () =>
            {
                this.StartSelectPossessionAccessoryIndex(actor);
            }));
        }
    }
}
