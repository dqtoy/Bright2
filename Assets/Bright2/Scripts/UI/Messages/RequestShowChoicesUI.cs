using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 選択肢UIの表示をリクエストするメッセージ
    /// </summary>
    public sealed class RequestShowChoicesUI : Message<RequestShowChoicesUI, string[]>
    {
        /// <summary>
        /// 選択肢
        /// </summary>
        public string[] Messages => this.param1;
    }
}
