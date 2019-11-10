using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// リストUIを表示した際のメッセージ
    /// </summary>
    public sealed class ShowListUI : Message<ShowListUI, ListUIController>
    {
        public ListUIController Controller => this.param1;
    }
}
