using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements
{
    /// <summary>
    /// 状態異常のインターフェイス
    /// </summary>
    public interface IAbnormalCondition
    {
        /// <summary>
        /// <paramref name="owner"/>に対して状態異常をアタッチする
        /// </summary>
        IObservable<Unit> Attach(Actor owner);

        void Detach();

        /// <summary>
        /// 状態異常のタイプを返す
        /// </summary>
        Constants.AbnormalStatus Type { get; }
    }
}
