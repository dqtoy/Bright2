using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;
using UniRx;
using System;
using UnityEngine.Playables;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// 宝箱を開けるクラス
    /// </summary>
    public sealed class OpenTreasureOnActorReactionOnTrigger : MonoBehaviour, IGameEvent, IActorReactionOnTriggerEnter2D, IActorReactionOnTriggerExit2D
    {
        [SerializeField]
        private List<string> includeTags = default;

        [SerializeField]
        private PlayableDirector playableDirector = default;

        [SerializeField]
        private Transform spawnPoint = default;

        [SerializeField]
        private List<WeaponRecord> weapons = default;

        [SerializeField]
        private List<AccessoryRecord> accessories = default;

        [SerializeField]
        private List<MaterialRecord> materials = default;

        [SerializeField]
        private int money = default;

        private Actor invokedActor;

        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            if(!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            actor.StatusController.SetGameEvent(this);
        }

        void IActorReactionOnTriggerExit2D.Do(Actor actor)
        {
            if (!this.includeTags.Contains(actor.tag))
            {
                return;
            }

            Assert.AreEqual(actor.StatusController.GameEvent, this);
            actor.StatusController.SetGameEvent(null);
        }

        void IGameEvent.Invoke(Actor invokedActor)
        {
            this.invokedActor = invokedActor;
            this.StartEffect();
        }

        /// <summary>
        /// エフェクトの再生が完了した際の処理
        /// </summary>
        /// <remarks>
        /// <see cref="SignalReceiver"/>により実行されます
        /// </remarks>
        public void OnCompleteEffect()
        {
            foreach (var w in this.weapons)
            {
                Broker.Global.Publish(RequestSpawnWeapon.Get(invokedActor, w, this.spawnPoint.position));
            }

            foreach (var a in this.accessories)
            {
                Broker.Global.Publish(RequestSpawnAccessory.Get(invokedActor, a, this.spawnPoint.position));
            }

            foreach(var m in this.materials)
            {
                Broker.Global.Publish(RequestSpawnMaterial.Get(invokedActor, m, this.spawnPoint.position));
            }

            if (this.money > 0)
            {
                Broker.Global.Publish(RequestSpawnMoney.Get(invokedActor, this.money, this.spawnPoint.position));
            }

            Destroy(this.gameObject);
            invokedActor.StatusController.SetGameEvent(null);
        }

        private void StartEffect()
        {
            this.playableDirector.Play();
        }
    }
}
