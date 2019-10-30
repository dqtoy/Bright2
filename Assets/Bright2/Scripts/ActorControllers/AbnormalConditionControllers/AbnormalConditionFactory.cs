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
                default:
                    Assert.IsTrue(false, $"{type}は未対応です");
                    return null;
            }
        }
    }
}
