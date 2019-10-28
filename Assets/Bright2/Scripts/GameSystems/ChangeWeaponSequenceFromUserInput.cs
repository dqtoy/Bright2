using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ユーザーの入力によって<see cref="Actor"/>の装備中の武器を切り替えるシーケンスを制御するクラス
    /// </summary>
    public sealed class ChangeWeaponSequenceFromUserInput : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<RequestChangeWeaponSequenceFromUserInput>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.StartSequence(x.Actor);
                })
                .AddTo(this);
        }

        private void StartSequence(Actor actor)
        {
            Broker.Global.Publish(RequestShowWeaponGridUI.Get(actor.StatusController.PossessionWeapons));

            Broker.Global.Receive<SelectInstanceWeaponIndex>()
                .TakeUntil(Broker.Global.Receive<HideWeaponGridUI>())
                .SubscribeWithState(actor, (x, _actor) =>
                {
                    Debug.Log($"SelectIndex = {x.Index}");
                })
                .AddTo(this);
        }
    }
}
