using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 攻撃のリクエストを行うAI
    /// </summary>
    [CreateAssetMenu(fileName = "RequestFire", menuName = "Bright2/AI/Elements/RequestFire")]
    public sealed class RequestFire : ScriptableAIElement
    {
        [SerializeField]
        private int equipmentIndex = default;

        public override void Enter(Actor owner, ActorAIController ownerAI)
        {
            base.Enter(owner, ownerAI);
            
            this.GetObserver(owner)
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    _owner.Broker.Publish(Messages.RequestFire.Get(_this.equipmentIndex));
                })
                .AddTo(this.events);
        }
    }
}
