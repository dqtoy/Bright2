using System;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.ActorControllers.States;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="IActorState"/>に関する拡張関数
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// <see cref="RequestMove"/>メッセージを受信して移動を行う
        /// </summary>
        public static IDisposable ReceiveRequestMoveOnMove(this IActorState self)
        {
            return self.Owner.Broker.Receive<RequestMove>()
                .SubscribeWithState(self, (x, _this) =>
                {
                    var velocity = Vector2.zero;
                    var moveSpeed = _this.Owner.Context.BasicStatus.MoveSpeed;
                    velocity.x = x.Direction.x > 0.0f ? moveSpeed : -moveSpeed;

                    _this.Owner.Movement.AddMove(velocity * Time.deltaTime);
                })
                .AddTo(self.Events);
        }
    }
}
