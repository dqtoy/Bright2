using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// リストUIを非表示にした際のメッセージ
    /// </summary>
    public sealed class HideListUI : Message<HideListUI, ListUIController, HideListUI.HidePatternType>
    {
        public enum HidePatternType
        {
            /// <summary><see cref="RequestHideListUI"/>イベントによってキャンセル</summary>
            FromRequest,

            /// <summary>ユーザーの入力によってキャンセル</summary>
            FromUserInput,
        }

        public ListUIController Controller => this.param1;

        public HidePatternType HidePattern => this.param2;
    }
}
