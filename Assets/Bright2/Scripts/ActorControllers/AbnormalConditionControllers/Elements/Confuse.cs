using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements
{
    /// <summary>
    /// 状態異常の混乱を制御するクラス
    /// </summary>
    public sealed class Confuse : AbnormalCondition
    {
        public override Constants.AbnormalStatus Type => Constants.AbnormalStatus.Confuse;

        public override IObservable<Unit> Attach(Actor owner)
        {
            return Observable.Timer(TimeSpan.FromSeconds(owner.Context.AbnormalCondition.Confuse.Duration)).AsUnitObservable();
        }

        public override void Detach()
        {
        }
    }
}
