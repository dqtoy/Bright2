using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Messages
{
    /// <summary>
    /// <see cref="Gimmick"/>が起動した際のメッセージ
    /// </summary>
    public sealed class ActivateGimmick : Message<ActivateGimmick, Actor>
    {
        /// <summary>
        /// <see cref="Gimmick"/>を起動した<see cref="Actor"/>
        /// </summary>
        public Actor Owner => this.param1;
    }
}
