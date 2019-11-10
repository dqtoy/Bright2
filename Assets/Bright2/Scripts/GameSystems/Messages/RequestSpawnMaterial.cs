using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// 素材の生成をリクエストするメッセージ
    /// </summary>
    public sealed class RequestSpawnMaterial : Message<RequestSpawnMaterial, Actor, MaterialRecord, Vector3>
    {
        public Actor Owner => this.param1;

        public MaterialRecord MaterialRecord => this.param2;

        public Vector3 SpawnPosition => this.param3;
    }
}
