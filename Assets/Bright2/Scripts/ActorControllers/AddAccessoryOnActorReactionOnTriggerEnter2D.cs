using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 衝突した<see cref="Actor"/>に対してアクセサリーを追加するクラス
    /// </summary>
    public sealed class AddAccessoryOnActorReactionOnTriggerEnter2D : MonoBehaviour, IActorReactionOnTriggerEnter2D, IAddAccessory
    {
        [SerializeField]
        private List<string> includeTags = new List<string>();

        private AccessoryRecord accessory;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.IsNotNull(this.accessory);
            actor.StatusController.AddAccessory(this.accessory);
        }

        void IAddAccessory.Setup(AccessoryRecord accessory)
        {
            this.accessory = accessory;
        }
    }
}
