using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using HK.Framework.Text;
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

        public override void Invoke(ISequenceGameEvent owner)
        {
            Broker.Global.Publish(RequestTalk.Get(this.message.Get));
        }
    }
}
