using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements
{
    /// <summary>
    /// 状態異常の麻痺を制御するクラス
    /// </summary>
    public sealed class Paralysis : AbnormalCondition
    {
        public override Constants.AbnormalStatus Type => Constants.AbnormalStatus.Paralysis;

        public override IObservable<Unit> Attach(Actor owner)
        {
            return Observable.Timer(TimeSpan.FromSeconds(owner.Context.AbnormalCondition.Paralysis.Duration)).AsUnitObservable();
        }

        public override void Detach()
        {
        }
    }
}
