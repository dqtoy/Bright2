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
                    _this.AddMove(x.Direction);
                })
                .AddTo(self.Events);
        }

        public static void AddMove(this IActorState self, Vector2 direction)
        {
            var velocity = Vector2.zero;
            var moveSpeed = self.Owner.Context.BasicStatus.MoveSpeed;
            velocity.x = direction.x > 0.0f ? moveSpeed : -moveSpeed;

            self.Owner.Movement.AddMove(velocity * Time.deltaTime);
        }
    }
}
