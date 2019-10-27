using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// ゲームイベントの実行をリクエストするメッセージ
    /// </summary>
    public sealed class RequestInvokeGameEvent : Message<RequestInvokeGameEvent>
    {
    }
}
