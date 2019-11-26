using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 衝突した<see cref="Actor"/>に対して大事なアイテムを追加するクラス
    /// </summary>
    public sealed class AddImportantItemOnActorReactionOnTriggerEnter2D : MonoBehaviour, IActorReactionOnTriggerEnter2D, IAddImportantItem
    {
        [SerializeField]
        private List<string> includeTags = new List<string>();

        private ImportantItemRecord importantItem;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.IsNotNull(this.importantItem);
            actor.StatusController.AddImportantItem(this.importantItem);
        }

        void IAddImportantItem.Setup(ImportantItemRecord importantItem)
        {
            this.importantItem = importantItem;
        }
    }
}
