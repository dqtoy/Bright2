using HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers
{
    /// <summary>
    /// 状態異常を生成するクラス
    /// </summary>
    public static class AbnormalConditionFactory
    {
        public static IAbnormalCondition Create(Constants.AbnormalStatus type)
        {
            switch(type)
            {
                case Constants.AbnormalStatus.Poison:
                    return new Poison();
                case Constants.AbnormalStatus.Paralysis:
                    return new Paralysis();
                case Constants.AbnormalStatus.Confuse:
                    return new Confuse();
                case Constants.AbnormalStatus.Fear:
                    return new Fear();
                case Constants.AbnormalStatus.DeadlyPoison:
                    return new DeadlyPoison();
                case Constants.AbnormalStatus.FireSpread:
                    return new FireSpread();
                default:
                    Assert.IsTrue(false, $"{type}は未対応です");
                    return null;
            }
        }
    }
}
