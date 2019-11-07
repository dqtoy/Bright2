using HK.Bright2.UIControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// 会話が開始した際のメッセージ
    /// </summary>
    public sealed class StartTalk : Message<StartTalk, TalkUIController>
    {
        public TalkUIController TalkUIController => this.param1;
    }
}
