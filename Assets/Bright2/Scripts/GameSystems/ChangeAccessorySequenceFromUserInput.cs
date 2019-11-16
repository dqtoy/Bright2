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
            Broker.Global.Publish(RequestShowGridUI.Get(items));

            Broker.Global.Receive<DecidedGridIndex>()
                .Take(1)
                .TakeUntil(Broker.Global.Receive<HideGridUI>())
                .SubscribeWithState2(this, actor, (x, _this, _actor) =>
                {
                    _this.possessionAccessoryIndex = x.Index;

                    // インデックスが決定したため終了処理は解放する
                    _this.publishEndMessage.Dispose();
                    _this.publishEndMessage = null;
                    
                    Broker.Global.Publish(RequestHideGridUI.Get());
                    _this.StartChangeEquippedAccessory(_actor);
                })
                .AddTo(this);

            this.publishEndMessage = Broker.Global.Receive<HideGridUI>()
                .Take(1)
                .TakeUntil(Broker.Global.Receive<DecidedGridIndex>())
                .Subscribe(_ =>
                {
                    Broker.Global.Publish(EndChangeAccessorySequenceFromUserInput.Get());
                })
                .AddTo(this);
        }

        private void StartChangeEquippedAccessory(Actor actor)
        {
            var items = actor.StatusController.EquippedAccessoryIcons;
            Broker.Global.Publish(RequestShowGridUI.Get(items));

            Broker.Global.Receive<DecidedGridIndex>()
                .Take(1)
                .TakeUntil(Broker.Global.Receive<HideGridUI>())
                .SubscribeWithState2(this, actor, (x, _this, _actor) =>
                {
                    _actor.StatusController.ChangeEquippedAccessory(x.Index, _this.possessionAccessoryIndex);
                    Broker.Global.Publish(RequestHideGridUI.Get());
                })
                .AddTo(this);

            Broker.Global.Receive<HideGridUI>()
                .Take(1)
                .SubscribeWithState2(this, actor, (x, _this, _actor) =>
                {
                    _this.StartSelectPossessionAccessoryIndex(_actor);
                })
                .AddTo(this);
        }
    }
}
