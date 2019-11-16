using System;
using System.Collections.Generic;
using HK.Framework.EventSystems;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// リストUIの表示をリクエストするメッセージ
    /// </summary>
    public sealed class RequestShowListUI : Message<RequestShowListUI, IEnumerable<IViewableList>, Action<int>, Action>
    {
        public IEnumerable<IViewableList> Items => this.param1;

        public Action<int> OnDecidedIndex => this.param2;

        public Action OnCancelFromUserInput => this.param3;
    }
}
