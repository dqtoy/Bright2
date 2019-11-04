using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// グリッドUIを表示した際のメッセージ
    /// </summary>
    public sealed class ShowGridUI : Message<ShowGridUI, GridUIController>
    {
        public GridUIController Controller => this.param1;
    }
}
