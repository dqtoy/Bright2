using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// 会話をするシーケンスゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/Element/Talk")]
    public sealed class Talk : SequenceGameEventElement
    {
        [SerializeField]
        private StringAsset.Finder message = default;

        [SerializeField]
        private SequenceGameEventElement nextElement = default;

        public override void Invoke(ISequenceGameEvent owner)
        {
            Broker.Global.Publish(RequestShowTalk.Get(this.message.Get));
            Broker.Global.Receive<EndTalk>()
                .Take(1)
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    _owner.Next(_this.nextElement);
                });
        }
    }
}
