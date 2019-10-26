using System;
using HK.Bright2.ActorControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="Collider2D.enabled"/>をタイマーで制御するデコレーター
    /// </summary>
    public sealed class SetEnableCollider2DOnTimer : MonoBehaviour, IGimmickDecorator
    {
        [SerializeField]
        private Collider2D target = default;

        [SerializeField]
        private bool isEnable = default;

        [SerializeField]
        private float delaySeconds = default;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.target.enabled = !this.isEnable;
            Observable.Timer(TimeSpan.FromSeconds(this.delaySeconds))
                .TakeUntilDisable(owner)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.target.enabled = _this.isEnable;
                })
                .AddTo(owner);
        }
    }
}
