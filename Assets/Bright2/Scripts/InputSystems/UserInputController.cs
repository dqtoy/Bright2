using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.InputSystems
{
    /// <summary>
    /// ユーザーの入力を制御するクラス
    /// </summary>
    public sealed class UserInputController : MonoBehaviour
    {
        private readonly Stack<IControllableUserInput> controllers = new Stack<IControllableUserInput>();

        private Actor actor;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.tag == Tags.Name.Player)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.actor = x.Actor;
                    _this.controllers.Push(new ActorControlOnUserInput(_this.actor));
                })
                .AddTo(this);

            Broker.Global.Receive<ShowWeaponGridUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.controllers.Push(x.Controller);
                })
                .AddTo(this);

            Broker.Global.Receive<HideWeaponGridUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    Assert.AreEqual(_this.controllers.Peek(), x.Controller);
                    _this.controllers.Pop();
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => this.controllers.Count > 0)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.controllers.Peek().UpdateInput();
                });
        }
    }
}
