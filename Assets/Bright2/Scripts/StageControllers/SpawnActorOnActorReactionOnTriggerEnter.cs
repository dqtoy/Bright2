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

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            foreach(var elementBundle in this.elementBundles)
            {
                foreach(var e in elementBundle.Elements)
                {
                    if (e.RightSpawnPoint != null)
                    {
                        Gizmos.DrawIcon(e.RightSpawnPoint.position, "Enemy.0000", false);
                    }
                    if (e.LeftSpawnPoint != null)
                    {
                        Gizmos.DrawIcon(e.LeftSpawnPoint.position, "Enemy.0000", true);
                    }
                }
            }
        }
#endif

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
            this.SpawnRecursive(0, actor.StatusController.Direction);
        }

        private void SpawnRecursive(int index, Constants.Direction direction)
        {
            Assert.IsTrue(this.elementBundles.Count > index);

            var elementBundle = this.elementBundles[index];
            var element = elementBundle.Elements.Lottery();
            var actor = element.Prefab.Spawn(element.GetSpawnPoint(direction).position);
            ++index;
            if (this.elementBundles.Count > index)
            {
                actor.Broker.Receive<Died>()
                    .SubscribeWithState2(this, index, (_, _this, i) =>
                    {
                        _this.SpawnRecursive(i, direction);
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
            private Transform rightSpawnPoint = default;
            public Transform RightSpawnPoint => this.rightSpawnPoint;

            [SerializeField]
            private Transform leftSpawnPoint = default;
            public Transform LeftSpawnPoint => this.leftSpawnPoint;

            int IWeightLottery.Weight => this.weight;

            public Transform GetSpawnPoint(Constants.Direction direction)
            {
                Assert.IsTrue(direction == Constants.Direction.Left || direction == Constants.Direction.Right);

                return direction == Constants.Direction.Right ? this.rightSpawnPoint : this.leftSpawnPoint;
            }
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
