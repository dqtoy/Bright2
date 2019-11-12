using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// <see cref="Actor"/>が死亡した際のメッセージ
    /// </summary>
    public sealed class Died : Message<Died, Actor>
    {
        /// <summary>
        /// <see cref="Died"/>メッセージを発行する<see cref="Actor"/>を倒した<see cref="Actor"/>
        /// </summary>
        public Actor Attacker => this.param1;
    }
}
