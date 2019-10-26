using System;
using HK.Bright2.ActorControllers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="IGimmickDecorator.OnActivate(Gimmick, ActorControllers.Actor)"/>のタイミングで一定間隔で別のギミックを生成するデコレーター
    /// </summary>
    public sealed class CreateGimmickInterval : MonoBehaviour, IGimmickDecorator
    {
        [SerializeField]
        private Gimmick otherGimmick = default;

        /// <summary>
        /// 生成したギミックの姿勢の同期を担う<see cref="Transform"/>
        /// </summary>
        [SerializeField]
        private Transform syncTransform = default;

        [SerializeField]
        private int limit = default;

        [SerializeField]
        private float interval = default;

        [SerializeField]
        private float initialDuration = default;

        private float duration;

        private int count;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.duration = this.initialDuration;
            this.count = 0;
            owner.UpdateAsObservable()
                .TakeUntilDisable(owner)
                .Where(_ => this.count < this.limit)
                .SubscribeWithState2(this, gimmickOwner, (_, _this, _gimmickOwner) =>
                {
                    _this.duration += Time.deltaTime;
                    if (_this.duration >= _this.interval)
                    {
                        _this.duration = 0.0f;
                        _this.count++;
                        _this.CreateOtherGimmick(_gimmickOwner);
                    }
                });
        }

        private void CreateOtherGimmick(Actor gimmickOwner)
        {
            var gimmick = this.otherGimmick.Rent();
            gimmick.transform.position = this.syncTransform.position;
            gimmick.transform.rotation = this.syncTransform.rotation;
            gimmick.Activate(gimmickOwner);
        }
    }
}
