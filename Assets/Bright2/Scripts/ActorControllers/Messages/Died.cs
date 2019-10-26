using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// <see cref="Actor"/>が死亡した際のメッセージ
    /// </summary>
    public sealed class Died : Message<Died>
    {
    }
}
