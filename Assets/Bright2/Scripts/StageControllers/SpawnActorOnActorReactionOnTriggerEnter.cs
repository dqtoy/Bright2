using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using UniRx;
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
        private List<ElementBundle> elementBundles = default;

        private bool isSpawned = false;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if (this.isSpawned)
            {
                return;
            }

            if (!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            this.isSpawned = true;
            this.SpawnRecursive(0);
        }

        private void SpawnRecursive(int index)
        {
            Assert.IsTrue(this.elementBundles.Count > index);

            var elementBundle = this.elementBundles[index];
            var element = elementBundle.Elements.Lottery();
            var actor = element.Prefab.Spawn(element.SpawnPoint.position);
            ++index;
            if (this.elementBundles.Count > index)
            {
                actor.Broker.Receive<Died>()
                    .SubscribeWithState2(this, index, (_, _this, i) =>
                    {
                        _this.SpawnRecursive(i);
                    })
                    .AddTo(actor);
            }
        }

        [Serializable]
        public class Element : IWeightLottery
        {
            [SerializeField]
            private Actor prefab = default;
            public Actor Prefab => this.prefab;

            [SerializeField]
            private int weight = default;

            [SerializeField]
            private Transform spawnPoint = default;
            public Transform SpawnPoint => this.spawnPoint;

            int IWeightLottery.Weight => this.weight;
        }
        
        [Serializable]
        public class ElementBundle
        {
            [SerializeField]
            private List<Element> elements = default;
            public List<Element> Elements => this.elements;
        }
    }
}
