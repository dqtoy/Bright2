using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 衝突した<see cref="Actor"/>に対して装備品を追加するクラス
    /// </summary>
    public sealed class AddEquipmentOnActorReactionOnTriggerEnter2D : MonoBehaviour, IActorReactionOnTriggerEnter2D, IAddEquipment
    {
        [SerializeField]
        private List<string> includeTags = new List<string>();

        private EquipmentRecord equipment;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.IsNotNull(this.equipment);
            actor.StatusController.AddEquipment(this.equipment);
        }

        void IAddEquipment.Setup(EquipmentRecord equipment)
        {
            this.equipment = equipment;
        }
    }
}
