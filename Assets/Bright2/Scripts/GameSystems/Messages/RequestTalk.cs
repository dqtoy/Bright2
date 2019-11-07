using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// 会話のリクエストを行うメッセージ
    /// </summary>
    public sealed class RequestTalk : Message<RequestTalk, string>
    {
        public string Message => this.param1;
    }
}
