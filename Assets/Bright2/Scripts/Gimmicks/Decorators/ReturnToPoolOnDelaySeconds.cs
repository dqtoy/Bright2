using System;
using HK.Bright2.ActorControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// 指定した秒数後に<see cref="Gimmick"/>を回収するクラス
    /// </summary>
    public sealed class ReturnToPoolOnDelaySeconds : MonoBehaviour, IGimmickDecorator
    {
        [SerializeField]
        private float delaySeconds = default;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            Observable.Timer(TimeSpan.FromSeconds(this.delaySeconds))
                .SubscribeWithState(owner, (_, _owner) =>
                {
                    _owner.Return();
                })
                .AddTo(owner);
        }
    }
}
