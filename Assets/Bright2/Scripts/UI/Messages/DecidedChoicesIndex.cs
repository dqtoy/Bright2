using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 選択肢が決定した際のメッセージ
    /// </summary>
    public sealed class DecidedChoicesIndex : Message<DecidedChoicesIndex, int>
    {
        public int Index => this.param1;
    }
}
