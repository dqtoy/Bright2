using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// ジャンプするAI
    /// </summary>
    [CreateAssetMenu(fileName = "Jump", menuName = "Bright2/AI/Elements/Jump")]
    public sealed class Jump : ScriptableAIElement
    {
        public override void Enter(Actor owner)
        {
            owner.UpdateAsObservable()
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    _owner.Broker.Publish(RequestJump.Get());
                })
                .AddTo(this.events);
        }
    }
}
