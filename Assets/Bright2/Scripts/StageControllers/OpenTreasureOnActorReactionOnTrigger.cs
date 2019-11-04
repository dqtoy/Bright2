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

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class OpenTreasureOnActorReactionOnTrigger : MonoBehaviour, IGameEvent, IActorReactionOnTriggerEnter2D, IActorReactionOnTriggerExit2D
    {
        [SerializeField]
        private List<string> includeTags = default;

        [SerializeField]
        private Transform effectRoot = default;

        [SerializeField]
        private Transform spawnPoint = default;

        [SerializeField]
        private Vector3 effectEndValueRelative = default;

        [SerializeField]
        private float effectDuration = default;

        [SerializeField]
        private Ease effectEase = default;

        [SerializeField]
        private GameObject openEffect = default;

        [SerializeField]
        private float openEffectDelaySeconds = default;

        [SerializeField]
        private List<WeaponRecord> weapons = default;

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
            this.StartEffect(invokedActor);
        }

        private void StartEffect(Actor invokedActor)
        {
            this.effectRoot.DOLocalMove(this.effectRoot.localPosition + this.effectEndValueRelative, this.effectDuration)
                .SetEase(this.effectEase)
                .OnComplete(() => this.CompleteEffect(invokedActor));

            Observable.Timer(TimeSpan.FromSeconds(this.openEffectDelaySeconds))
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.openEffect.SetActive(true);
                })
                .AddTo(this);
        }

        private void CompleteEffect(Actor invokedActor)
        {
            foreach (var w in this.weapons)
            {
                Broker.Global.Publish(RequestSpawnWeapon.Get(invokedActor, w, this.spawnPoint.position));
            }

            Destroy(this.gameObject);
            invokedActor.StatusController.SetGameEvent(null);
        }
    }
}
