using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// AIを切り替えるAI
    /// </summary>
    [CreateAssetMenu(fileName = "ChangeAI", menuName = "Bright2/AI/Elements/ChangeAI")]
    public sealed class ChangeAI : ScriptableAIElement
    {
        [SerializeField]
        private string nextAIName = default;

        public override void Enter(Actor owner, ActorAIController ownerAI)
        {
            base.Enter(owner, ownerAI);
            
            this.GetObserver(owner)
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    ownerAI.ChangeAI(this.nextAIName);
                })
                .AddTo(this.events);
        }
    }
}
