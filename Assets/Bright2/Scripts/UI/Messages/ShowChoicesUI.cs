using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 選択肢UIを表示した際のメッセージ
    /// </summary>
    public sealed class ShowChoicesUI : Message<ShowChoicesUI, ChoicesUIController>
    {
        public ChoicesUIController Controller => this.param1;
    }
}
