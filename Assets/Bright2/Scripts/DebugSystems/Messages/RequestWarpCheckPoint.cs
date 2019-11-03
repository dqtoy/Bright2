using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

#if BRIGHT_DEBUG
namespace HK.Bright2.DebugSystems.Messages
{
    /// <summary>
    /// デバッグでチェックポイントへワープをリクエストするメッセージ
    /// </summary>
    public sealed class RequestWarpCheckPoint : Message<RequestWarpCheckPoint, Actor, int>
    {
        public Actor WarpTarget => this.param1;

        public int WarpIndex => this.param2;
    }
}
#endif