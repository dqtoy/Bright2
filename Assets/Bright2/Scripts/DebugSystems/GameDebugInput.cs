using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Bright2.DebugSystems.Messages;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.DebugSystems
{
    /// <summary>
    /// ゲームデバッグする入力を制御するクラス
    /// </summary>
    public sealed class GameDebugInput : MonoBehaviour
    {
        [SerializeField]
        private string[] choicesMessages = default;

        private Actor actor;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.actor = x.Actor;
                })
                .AddTo(this);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Broker.Global.Publish(RequestChangeWeaponSequenceFromUserInput.Get(this.actor));
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Broker.Global.Publish(RequestChangeAccessorySequenceFromUserInput.Get(this.actor));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Broker.Global.Publish(RequestShowChoicesUI.Get(this.choicesMessages));
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                Broker.Global.Publish(RequestShowListUI.Get(this.actor.StatusController.PossessionAccessories));
            }

            this.InputOnCtrlKey();
        }

        void InputOnCtrlKey()
        {
            if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                return;
            }

            for (var i = 0; i < 10; i++)
            {
                var keyCode = KeyCode.Alpha0 + i;
                if(Input.GetKeyDown(keyCode))
                {
                    Broker.Global.Publish(RequestWarpCheckPoint.Get(this.actor, i));
                }
            }
        }
    }
}
