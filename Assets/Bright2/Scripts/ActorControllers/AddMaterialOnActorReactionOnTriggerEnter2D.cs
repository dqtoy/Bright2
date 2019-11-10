using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 衝突した<see cref="Actor"/>に対して素材を追加するクラス
    /// </summary>
    public sealed class AddMaterialOnActorReactionOnTriggerEnter2D : MonoBehaviour, IActorReactionOnTriggerEnter2D, IAddMaterial
    {
        [SerializeField]
        private List<string> includeTags = new List<string>();

        private MaterialRecord material;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.IsNotNull(this.material);
            actor.StatusController.AddMaterial(this.material, 1);
        }

        void IAddMaterial.Setup(MaterialRecord material)
        {
            this.material = material;
        }
    }
}
