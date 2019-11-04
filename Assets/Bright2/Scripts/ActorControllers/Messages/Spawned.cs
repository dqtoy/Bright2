using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.GameSystems.Messages;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// <see cref="Actor"/>が生成された際のメッセージ
    /// </summary>
    /// <remarks>
    /// <see cref="SpawnedActor"/>とは別でこちらは<see cref="Actor"/>に対して通知します
    /// </remarks>
    public sealed class Spawned : Message<Spawned>
    {
    }
}
