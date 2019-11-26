using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// 大事なアイテムの生成をリクエストするメッセージ
    /// </summary>
    public sealed class RequestSpawnImportantItem : Message<RequestSpawnImportantItem, Actor, ImportantItemRecord, Vector3>
    {
        public Actor Owner => this.param1;

        public ImportantItemRecord ImportantItemRecord => this.param2;

        public Vector3 SpawnPosition => this.param3;
    }
}
