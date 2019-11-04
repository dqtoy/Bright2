using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// グリッドUIのインデックスが決定された際のメッセージ
    /// </summary>
    public sealed class DecidedGridIndex : Message<DecidedGridIndex, int>
    {
        public int Index => this.param1;
    }
}
