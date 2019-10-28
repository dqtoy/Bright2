using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 衝突した<see cref="Actor"/>に対して武器を追加するクラス
    /// </summary>
    public sealed class AddWeaponOnActorReactionOnTriggerEnter2D : MonoBehaviour, IActorReactionOnTriggerEnter2D, IAddWeapon
    {
        [SerializeField]
        private List<string> includeTags = new List<string>();

        private WeaponRecord weapon;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.IsNotNull(this.weapon);
            actor.StatusController.AddWeapon(this.weapon);
        }

        void IAddWeapon.Setup(WeaponRecord weapon)
        {
            this.weapon = weapon;
        }
    }
}
