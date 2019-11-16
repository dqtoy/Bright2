using System.Collections.Generic;
using HK.Framework.EventSystems;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// リストUIの表示をリクエストするメッセージ
    /// </summary>
    public sealed class RequestShowListUI : Message<RequestShowListUI, IEnumerable<IViewableList>>
    {
        public IEnumerable<IViewableList> Items => this.param1;
    }
}
