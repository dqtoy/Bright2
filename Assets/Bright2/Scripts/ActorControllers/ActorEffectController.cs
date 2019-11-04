using System;
using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に対して様々なエフェクトを生成するクラス
    /// </summary>
    public sealed class ActorEffectController
    {
        private readonly Actor owner;

        /// <summary>
        /// <see cref="Actor"/>が生成されたあと、<see cref="Actor"/>が表示されるまでの遅延時間（秒）
        /// </summary>
        private const float SpawnedVisibleDelaySeconds = 0.3f;

        public ActorEffectController(Actor owner)
        {
            this.owner = owner;

            this.owner.Broker.Receive<Spawned>()
                .Where(_ => this.owner.CompareTag(Tags.Name.Enemy))
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.StartSpawnedEffect();
                })
                .AddTo(this.owner);

            this.owner.Broker.Receive<TakedDamage>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var effect = _this.owner.Context.Effects.TakedDamage.Rent();
                    effect.transform.position = x.Result.GenerationSource;
                })
                .AddTo(this.owner);
        }

        private void StartSpawnedEffect()
        {
            var effect = this.owner.Context.Effects.Spawned.Rent();
            effect.transform.position = this.owner.CachedTransform.position;

            this.owner.gameObject.SetActive(false);
            Observable.Timer(TimeSpan.FromSeconds(SpawnedVisibleDelaySeconds))
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.gameObject.SetActive(true);
                })
                .AddTo(this.owner);
        }
    }
}
