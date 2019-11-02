using System;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// 指定した秒数後に<see cref="Gimmick"/>を回収するクラス
    /// </summary>
    public sealed class ReturnToPoolOnDelaySeconds : MonoBehaviour, IGimmickDecorator, IAffectedSpeedUp
    {
        [SerializeField]
        private float delaySeconds = default;

        private float currentDelaySeconds;

        void IAffectedSpeedUp.Affected(float rate)
        {
            this.currentDelaySeconds = Calculator.GetFireSpeedUp(this.delaySeconds, rate);
        }

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            Observable.Timer(TimeSpan.FromSeconds(this.currentDelaySeconds))
                .TakeUntilDisable(owner)
                .SubscribeWithState(owner, (_, _owner) =>
                {
                    _owner.Return();
                })
                .AddTo(owner);
        }
    }
}
