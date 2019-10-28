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
                    Broker.Global.Publish(RequestShowWeaponGridUI.Get(x.Actor.StatusController.PossessionWeapons));
                })
                .AddTo(this);
        }
    }
}
