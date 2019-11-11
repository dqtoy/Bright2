using System;
using System.Linq;
using HK.Bright2.ActorControllers;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// 選択肢を出すシーケンスゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/Element/Choices")]
    public sealed class Choices : SequenceGameEventElement
    {
        [SerializeField]
        private Element[] elements = default;

        public override void Invoke(ISequenceGameEvent owner, Actor invoker)
        {
            var messages = this.elements.Select(x => x.message.Get).ToArray();
            Broker.Global.Publish(RequestShowChoicesUI.Get(messages));

            Broker.Global.Receive<DecidedChoicesIndex>()
                .Take(1)
                .SubscribeWithState3(this, owner, invoker, (x, _this, _owner, _invoker) =>
                {
                    _owner.Next(_this.elements[x.Index].nextSequence, _invoker);
                })
                .AddTo(invoker);
        }

        [Serializable]
        public class Element
        {
            public StringAsset.Finder message = default;

            public SequenceGameEventElement nextSequence = default;
        }
    }
}
