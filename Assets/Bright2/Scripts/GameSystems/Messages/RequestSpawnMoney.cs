using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// お金の生成をリクエストするメッセージ
    /// </summary>
    public sealed class RequestSpawnMoney : Message<RequestSpawnMoney, Actor, int, Vector3>
    {
        public Actor Owner => this.param1;

        public int Amount => this.param2;

        public Vector3 SpawnPosition => this.param3;
    }
}
