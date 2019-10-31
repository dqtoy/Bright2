using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements
{
    /// <summary>
    /// 状態異常の猛毒を制御するクラス
    /// </summary>
    public sealed class DeadlyPoison : PoisonBase
    {
        public override Constants.AbnormalStatus Type => Constants.AbnormalStatus.DeadlyPoison;

        protected override int DamageSplitCount => 10;

        protected override ActorContext.AbnormalConditionParameters.PoisonParameter GetParameter(Actor owner)
        {
            return owner.Context.AbnormalCondition.DeadlyPoison;
        }
    }
}
