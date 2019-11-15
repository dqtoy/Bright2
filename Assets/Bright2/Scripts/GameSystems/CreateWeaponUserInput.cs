using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ユーザーの入力によって<see cref="Actor"/>に対して武器を追加するクラス
    /// </summary>
    public sealed class CreateWeaponUserInput : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<RequestCreateWeaponUserInput>()
                .SubscribeWithState(this, (x, _this) =>
                {

                })
                .AddTo(this);
        }
    }
}
