using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// グリッドUIを閉じた際のメッセージ
    /// </summary>
    public sealed class HideGridUI : Message<HideGridUI, GridUIController>
    {
        public GridUIController Controller => this.param1;
    }
}
