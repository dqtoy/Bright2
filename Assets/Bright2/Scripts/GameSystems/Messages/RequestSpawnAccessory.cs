using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// アクセサリーの生成をリクエストするメッセージ
    /// </summary>
    public sealed class RequestSpawnAccessory : Message<RequestSpawnAccessory, Actor, AccessoryRecord, Vector3>
    {
        public Actor Owner => this.param1;

        public AccessoryRecord AccessoryRecord => this.param2;

        public Vector3 SpawnPosition => this.param3;
    }
}
