using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// 会話のリクエストを行うメッセージ
    /// </summary>
    public sealed class RequestShowTalk : Message<RequestShowTalk, string>
    {
        public string Message => this.param1;
    }
}
