using System;
using HK.Bright2.ActorControllers;
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
    public static partial class Extensions
    {
        /// <summary>
        /// <see cref="RequestMove"/>メッセージを受信して移動を行う
        /// </summary>
        public static IDisposable ReceiveRequestMoveOnMove(this IActorState self, float moveSpeed)
        {
            return self.Owner.Broker.Receive<RequestMove>()
                .SubscribeWithState(self, (x, _this) =>
                {
                    _this.AddMove(x.Direction, moveSpeed);
                })
                .AddTo(self.Events);
        }

        /// <summary>
        /// <see cref="Actor"/>を移動させる
        /// </summary>
        public static void AddMove(this IActorState self, Vector2 direction, float moveSpeed)
        {
            var velocity = Vector2.zero;
            velocity.x = direction.x > 0.0f ? moveSpeed : -moveSpeed;

            self.Owner.Movement.AddMove(velocity * Time.deltaTime);
        }

        /// <summary>
        /// <see cref="Actor"/>の向きを移動方向と同期を取る
        /// </summary>
        public static void SyncActorDirection(this IActorState self)
        {
            self.Owner.Broker.Receive<Move>()
                .SubscribeWithState(self, (x, _this) =>
                {
                    var direction = x.Velocity.GetHorizontalDirection();
                    Assert.AreNotEqual(direction, Constants.Direction.None);
                    _this.Owner.StatusController.SetDirection(direction);
                    _this.Owner.ModelController.Turn(x.Velocity.GetHorizontalDirection());
                })
                .AddTo(self.Events);
        }

        /// <summary>
        /// <see cref="RequestFire"/>メッセージを受信して<see cref="ActorState.Name.Attack"/>ステートへ遷移する
        /// </summary>
        public static void ReceiveRequestFireOnChangeAttackState(this IActorState self, int index)
        {
            self.Owner.Broker.Receive<RequestFire>()
                .Where(_ => self.Owner.StatusController.EquippedEquipments[index].CanFire)
                .SubscribeWithState(self, (_, _this) =>
                {
                    _this.Owner.StateManager.Change(ActorState.Name.Attack);
                })
                .AddTo(self.Events);
        }
    }
}
