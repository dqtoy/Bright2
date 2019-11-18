using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// 装備しているアクセサリーを切り替えるシーケンスゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/Element/ChangeAccessoryUserInput")]
    public sealed class ChangeAccessoryUserInput : SequenceGameEventElement
    {
        [SerializeField]
        private SequenceGameEventElement nextElement = default;

        public override void Invoke(ISequenceGameEvent owner, Actor invoker)
        {
            Broker.Global.Publish(RequestChangeAccessorySequenceFromUserInput.Get(invoker));

            Broker.Global.Receive<EndChangeAccessorySequenceFromUserInput>()
                .Take(1)
                .SubscribeWithState3(this, owner, invoker, (_, _this, _owner, _invoker) =>
                {
                    _owner.Next(_this.nextElement, _invoker);
                })
                .AddTo(invoker);
        }
    }
}
