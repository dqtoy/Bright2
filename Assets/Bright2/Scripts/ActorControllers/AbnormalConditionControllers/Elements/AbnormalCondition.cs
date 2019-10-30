using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements
{
    /// <summary>
    /// 状態異常の抽象クラス
    /// </summary>
    public abstract class AbnormalCondition : IAbnormalCondition
    {
        public abstract Constants.AbnormalStatus Type { get; }

        public abstract IObservable<Unit> Attach(Actor owner);
        public abstract void Detach();
    }
}
