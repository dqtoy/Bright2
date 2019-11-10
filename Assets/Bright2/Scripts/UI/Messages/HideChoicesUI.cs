using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 選択肢UIを非表示にした際のメッセージ
    /// </summary>
    public sealed class HideChoicesUI : Message<HideChoicesUI, ChoicesUIController>
    {
        public ChoicesUIController Controller => this.param1;
    }
}
