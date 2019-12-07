using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;

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
            base.Enter(owner);
            
            this.GetObserver(owner)
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    _owner.Broker.Publish(RequestJump.Get());
                })
                .AddTo(this.events);
        }
    }
}
