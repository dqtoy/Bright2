using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// グリッドUIのインデックスが変更した際のメッセージ
    /// </summary>
    public sealed class ChangedGridIndex : Message<ChangedGridIndex, int>
    {
        public int Index => this.param1;
    }
}
