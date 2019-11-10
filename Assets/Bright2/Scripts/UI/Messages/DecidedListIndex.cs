using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// リストUIのインデックスが決定された際のメッセージ
    /// </summary>
    public sealed class DecidedListIndex : Message<DecidedListIndex, int>
    {
        public int Index => this.param1;
    }
}
