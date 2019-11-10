using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// リストUIを非表示にした際のメッセージ
    /// </summary>
    public sealed class HideListUI : Message<HideListUI, ListUIController>
    {
        public ListUIController Controller => this.param1;
    }
}
