using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.ItemControllers;
using UniRx;
using UnityEngine;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// <see cref="ItemModifier"/>を追加するAI
    /// </summary>
    [CreateAssetMenu(fileName = "AddItemModifier", menuName = "Bright2/AI/Elements/AddItemModifier")]
    public sealed class AddItemModifier : ScriptableAIElement
    {
        [SerializeField]
        private bool removeOnExit = default;

        [SerializeField]
        private List<ItemModifier> itemModifiers = default;

        private int addCount = 0;

        public override void Enter(Actor owner, ActorAIController ownerAI)
        {
            base.Enter(owner, ownerAI);

            this.addCount = 0;

            this.GetObserver(owner)
                .SubscribeWithState2(this, owner, (_, _this, _owner) =>
                {
                    foreach (var i in _this.itemModifiers)
                    {
                        owner.StatusController.AddOtherItemModifier(i);
                    }

                    _this.addCount++;
                })
                .AddTo(this.events);
        }

        public override void Exit(Actor owner, ActorAIController ownerAI)
        {
            base.Exit(owner, ownerAI);
            if(this.removeOnExit)
            {
                for (var i = 0; i < this.addCount; i++)
                {
                    foreach (var o in this.itemModifiers)
                    {
                        owner.StatusController.RemoveOtherItemModifier(o);
                    }
                }
            }
        }
    }
}
