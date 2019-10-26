﻿using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// <see cref="Actor"/>が移動したことを通知するメッセージ
    /// </summary>
    public sealed class Move : Message<Move, Vector2>
    {
        public Vector2 Velocity => this.param1;
    }

    /// <summary>
    /// <see cref="Actor"/>が停止したことを通知するメッセージ
    /// </summary>
    public sealed class Idle : Message<Idle>
    {
    }

    /// <summary>
    /// <see cref="Actor"/>に対してジャンプをリクエストするメッセージ
    /// </summary>
    public sealed class RequestJump : Message<RequestJump>
    {
    }

    /// <summary>
    /// <see cref="Actor"/>が地面に着地した際のメッセージ
    /// </summary>
    public sealed class Landed : Message<Landed>
    {
    }
}