using System;
using UniRx;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// AIを実行する条件を持つインターフェイス
    /// </summary>
    public interface IAICondition
    {
        /// <summary>
        /// 条件を満たした場合に発行されるオブザーバーを返す
        /// </summary>
        IObservable<Unit> Satisfy();
    }
}
