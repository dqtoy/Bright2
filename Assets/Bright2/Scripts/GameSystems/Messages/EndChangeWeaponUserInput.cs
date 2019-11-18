using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// ユーザーの入力によって武器を切り替えるシーケンスが終了した際のメッセージ
    /// </summary>
    public sealed class EndChangeWeaponUserInput : Message<EndChangeWeaponUserInput>
    {
    }
}
