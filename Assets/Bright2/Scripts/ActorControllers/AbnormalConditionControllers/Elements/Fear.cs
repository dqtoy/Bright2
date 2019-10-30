using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements
{
    /// <summary>
    /// 状態異常の恐怖を制御するクラス
    /// </summary>
    public sealed class Fear : AbnormalCondition
    {
        public override Constants.AbnormalStatus Type => Constants.AbnormalStatus.Fear;

        public override IObservable<Unit> Attach(Actor owner)
        {
            return Observable.Timer(TimeSpan.FromSeconds(owner.Context.AbnormalCondition.Fear.Duration)).AsUnitObservable();
        }

        public override void Detach()
        {
        }
    }
}
