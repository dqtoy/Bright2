using System;
using HK.Framework.EventSystems;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// リストUIの非表示をリクエストするメッセージ
    /// </summary>
    public sealed class RequestHideListUI : Message<RequestHideListUI, Action>
    {
        /// <summary>
        /// リストが閉じた際の処理
        /// </summary>
        public Action OnHide => this.param1;
    }
}
