﻿using System;
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
        public static IDisposable ReceiveRequestMoveOnMove(this IActorState self, Func<float> moveSpeedSelector)
        {
            return self.Owner.Broker.Receive<RequestMove>()
                .SubscribeWithState(self, (x, _this) =>
                {
                    _this.AddMove(x.Direction, moveSpeedSelector);
                })
                .AddTo(self.Events);
        }

        /// <summary>
        /// <see cref="Actor"/>を移動させる
        /// </summary>
        public static void AddMove(this IActorState self, Vector2 direction, Func<float> moveSpeedSelector)
        {
            var moveSpeed = moveSpeedSelector();
            var velocity = Vector2.zero;
            if(direction.x != 0.0f)
            {
                velocity.x = direction.x > 0.0f ? moveSpeed : -moveSpeed;
            }
            if(direction.y != 0.0f)
            {
                velocity.y = direction.y > 0.0f ? moveSpeed : -moveSpeed;
            }

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
        public static void ReceiveRequestFireOnChangeAttackState(this IActorState self)
        {
            self.Owner.Broker.Receive<RequestFire>()
                .Where(x => !self.Owner.AbnormalConditionController.Contains(Constants.AbnormalStatus.Fear))
                .Where(x => self.Owner.StatusController.EquippedWeapons[x.EquippedWeaponIndex].CanFire)
                .SubscribeWithState(self, (x, _this) =>
                {
                    _this.Owner.StateManager.Change(ActorState.Name.Attack, new StateAttackContext(x.EquippedWeaponIndex));
                })
                .AddTo(self.Events);
        }

        /// <summary>
        /// <see cref="RequestInvokeGameEvent"/>メッセージを受信してゲームイベントを実行する
        /// </summary>
        public static void ReceiveRequestInvokeGameEventOnInvokeGameEvent(this IActorState self)
        {
            self.Owner.Broker.Receive<RequestInvokeGameEvent>()
                .Where(_ => self.Owner.StatusController.GameEvent != null)
                .SubscribeWithState(self, (_, _this) =>
                {
                    _this.Owner.StatusController.GameEvent.Invoke(_this.Owner);
                })
                .AddTo(self.Events);
        }

        /// <summary>
        /// <see cref="RequestJump"/>を受信したらジャンプを行う
        /// </summary>
        public static void ReceiveRequestJumpOnJump(this IActorState self)
        {
            self.Owner.Broker.Receive<RequestJump>()
                .SubscribeWithState(self, (_, _this) =>
                {
                    _this.InvokeJump();
                })
                .AddTo(self.Events);
        }

        /// <summary>
        /// ジャンプを実行する
        /// </summary>
        public static void InvokeJump(this IActorState self)
        {
            if (!self.Owner.StatusController.CanJump)
            {
                return;
            }

            self.Owner.StatusController.AddJumpCount();
            self.Owner.Movement.SetGravity(Vector2.up * self.Owner.Context.BasicStatus.JumpPower);
        }

        public static void ReceiveRequestFallOneWayPlatforms(this IActorState self)
        {
            self.Owner.Broker.Receive<RequestFallOneWayPlatforms>()
                .Where(_ => self.Owner.Movement.IsGrounded)
                .SubscribeWithState(self, (_, _this) =>
                {
                    _this.Owner.Movement.IgnoreOneWayPlatformsThisFrame = true;
                    _this.Owner.Movement.SetGravity(Vector2.down * 5.0f);
                })
                .AddTo(self.Events);
        }
    }
}
