using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
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
        private IControllableUserInput current;

        private Actor actor;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.tag == Tags.Name.Player)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.actor = x.Actor;
                    _this.current = new ActorControlOnUserInput(_this.actor);
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => this.current != null)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.current.UpdateInput();
                });
        }
    }
}
