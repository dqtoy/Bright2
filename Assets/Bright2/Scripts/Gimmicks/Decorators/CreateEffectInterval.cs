using System;
using HK.Bright2.ActorControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// 一定間隔でエフェクトを生成するギミックデコレーター
    /// </summary>
    public sealed class CreateEffectInterval : MonoBehaviour, IGimmickDecorator
    {
        [SerializeField]
        private Transform syncTransform = default;

        [SerializeField]
        private PoolableEffect prefab = default;

        [SerializeField]
        private int limit = default;

        [SerializeField]
        private float interval = default;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            Observable.Interval(TimeSpan.FromSeconds(this.interval))
                .Take(this.limit)
                .TakeUntilDisable(owner)
                .SubscribeWithState(this, (_, _this) =>
                {
                    var effect = _this.prefab.Rent();
                    effect.transform.position = _this.syncTransform.position;
                    effect.transform.rotation = _this.syncTransform.rotation;
                })
                .AddTo(owner);
        }
    }
}
