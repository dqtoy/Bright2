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
    /// ユーザーの入力によって<see cref="Actor"/>の装備中の武器を切り替えるシーケンスを制御するクラス
    /// </summary>
    public sealed class ChangeWeaponSequenceFromUserInput : MonoBehaviour
    {
        private int possessionWeaponIndex;

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
                .SubscribeWithState2(this, actor, (x, _this, _actor) =>
                {
                    _this.possessionWeaponIndex = x.Index;
                    _this.ObserveBeginControlUserInputEquippedWeaponUI(_actor);
                    Broker.Global.Publish(RequestControlUserInputEquippedWeaponUI.Get());
                })
                .AddTo(this);
        }

        private void ObserveBeginControlUserInputEquippedWeaponUI(Actor actor)
        {
            Broker.Global.Receive<BeginControlUserInputEquippedWeaponUI>()
                .TakeUntil(Broker.Global.Receive<EndControlUserInputEquippedWeaponUI>())
                .SubscribeWithState2(this, actor, (x, _this, _actor) =>
                {
                    _this.ObserveSelectEquippedWeaponIndex(_actor, x.Controller);
                })
                .AddTo(this);
        }

        private void ObserveSelectEquippedWeaponIndex(Actor actor, EquippedWeaponUIController uiController)
        {
            Broker.Global.Receive<SelectEquippedWeaponIndex>()
                .TakeUntil(Broker.Global.Receive<EndControlUserInputEquippedWeaponUI>())
                .SubscribeWithState3(this, actor, uiController, (x, _this, _actor, _uiController) =>
                {
                    var instanceWeapon = _actor.StatusController.PossessionWeapons[_this.possessionWeaponIndex];
                    _actor.StatusController.ChangeEquippedWeapon(x.Index, instanceWeapon);
                    Broker.Global.Publish(EndControlUserInputEquippedWeaponUI.Get(_uiController));
                })
                .AddTo(this);
        }
    }
}
