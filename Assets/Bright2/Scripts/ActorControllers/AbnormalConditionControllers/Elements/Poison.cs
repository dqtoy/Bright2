using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements
{
    /// <summary>
    /// 状態異常の毒を制御するクラス
    /// </summary>
    public sealed class Poison : AbnormalCondition
    {
        /// <summary>
        /// ダメージを与える分割数
        /// </summary>
        private const int DamageSplitCount = 5;

        public override Constants.AbnormalStatus Type => Constants.AbnormalStatus.Poison;

        public override IObservable<Unit> Attach(Actor owner)
        {
            return Observable.Create<Unit>((observer) =>
            {
                var poisonContext = owner.Context.AbnormalCondition.Poison;

                // 定期的に毒ダメージを与える
                Observable.Interval(TimeSpan.FromSeconds(poisonContext.Duration / DamageSplitCount))
                    .Take(DamageSplitCount)
                    .SubscribeWithState(owner, (_, _owner) =>
                    {
                        var damage = (poisonContext.DamageRate * owner.StatusController.HitPointMax.Value) / DamageSplitCount;
                        owner.StatusController.TakeDamage((int)damage, owner.CachedTransform.position);
                    })
                    .AddTo(owner);

                // 毒時間を超えた場合に完了通知する
                Observable.Timer(TimeSpan.FromSeconds(poisonContext.Duration))
                    .SubscribeWithState(observer, (_, _observer) =>
                    {
                        _observer.OnNext(Unit.Default);
                        _observer.OnCompleted();
                    })
                    .AddTo(owner);

                return owner.OnDestroyAsObservable().Subscribe();
            });
        }

        public override void Detach()
        {
        }
    }
}
