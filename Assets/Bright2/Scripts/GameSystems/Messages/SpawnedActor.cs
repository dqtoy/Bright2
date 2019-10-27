using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// <see cref="Actor"/>が生成されたことを通知するメッセージ
    /// </summary>
    public sealed class SpawnedActor : Message<SpawnedActor, Actor>
    {
        public Actor Actor => this.param1;
    }
}
