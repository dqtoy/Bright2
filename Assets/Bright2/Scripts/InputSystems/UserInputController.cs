using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.StageControllers.Messages;
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
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.actor = x.Actor;
                    _this.controllers.Push(new ActorControlOnUserInput(_this.actor));
                })
                .AddTo(this);

            this.PushController<BeginChangeStage>(_ => NoneInput.Default);
            this.PopController<EndChangeStage>(_ => NoneInput.Default);

            this.PushController<ShowGridUI>(x => x.Controller);
            this.PopController<HideGridUI>(x => x.Controller);

            this.PushController<BeginControlUserInputEquippedWeaponUI>(x => x.Controller);
            this.PopController<EndControlUserInputEquippedWeaponUI>(x => x.Controller);

            this.PushController<StartSequenceGameEvent>(_ => NoneInput.Default);
            this.PopController<EndSequenceGameEvent>(_ => NoneInput.Default);

            this.PushController<StartTalk>(x => x.TalkUIController);
            this.PopController<EndTalk>(x => x.TalkUIController);

            this.UpdateAsObservable()
                .Where(_ => this.controllers.Count > 0)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.controllers.Peek().UpdateInput();

                    if(Input.GetKeyDown(KeyCode.U))
                    {
                        this.PrintControllers();
                    }
                });
        }

        private void PushController<T>(Func<T, IControllableUserInput> func)
        {
            Broker.Global.Receive<T>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.controllers.Push(func(x));
                })
                .AddTo(this);
        }

        private void PopController<T>(Func<T, IControllableUserInput> func)
        {
            Broker.Global.Receive<T>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var controller = func(x);
                    Assert.AreEqual(_this.controllers.Peek(), controller);
                    _this.controllers.Pop();
                })
                .AddTo(this);
        }

        private void PrintControllers()
        {
            Debug.Log("Begin IControllableUserInput =====");
            foreach(var i in this.controllers)
            {
                Debug.Log($"{i}");
            }
            Debug.Log("End IControllableUserInput   =====");
        }
    }
}
