using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SpawnActorOnActorReactionOnTriggerEnter : MonoBehaviour, IActorReactionOnTriggerEnter2D
    {
        [SerializeField]
        private List<string> includeTags = default;

        [SerializeField]
        private Transform spawnPoint = default;

        [SerializeField]
        private List<Element> elements = default;

        private bool isSpawned = false;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(this.isSpawned)
            {
                return;
            }

            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            var element = this.elements.Lottery();
            element.Prefab.Spawn(this.spawnPoint.position);
            this.isSpawned = true;
        }

        [Serializable]
        public class Element : IWeightLottery
        {
            [SerializeField]
            private Actor prefab = default;
            public Actor Prefab => this.prefab;

            [SerializeField]
            private int weight = default;

            int IWeightLottery.Weight => this.weight;
        }
    }
}
