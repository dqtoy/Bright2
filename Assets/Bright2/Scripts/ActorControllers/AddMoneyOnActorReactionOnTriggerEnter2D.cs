using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 衝突した<see cref="Actor"/>に対して所持金を加算するクラス
    /// </summary>
    public sealed class AddMoneyOnActorReactionOnTriggerEnter2D : MonoBehaviour, IActorReactionOnTriggerEnter2D
    {
        [SerializeField]
        private int money = default;

        [SerializeField]
        private List<string> includeTags = new List<string>();

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            actor.StatusController.Inventory.AddMoney(this.money);
        }
    }
}
