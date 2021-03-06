﻿using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// アクセサリー作成を行うシーケンスゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/Element/CreateAccessoryUserInput")]
    public sealed class CreateAccessoryUserInput : SequenceGameEventElement
    {
        [SerializeField]
        private SequenceGameEventElement nextElement = default;

        public override void Invoke(ISequenceGameEvent owner, Actor invoker)
        {
            Broker.Global.Publish(CreateAccessoryUserInputMessages.Request.Get(invoker));

            Broker.Global.Receive<CreateAccessoryUserInputMessages.End>()
                .Take(1)
                .SubscribeWithState3(this, owner, invoker, (_, _this, _owner, _invoker) =>
                {
                    _owner.Next(_this.nextElement, _invoker);
                })
                .AddTo(invoker);
        }
    }
}
