using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="IWeightLottery"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static T Lottery<T>(this IEnumerable<T> list)
            where T : IWeightLottery
        {
            var totalWeight = 0;
            foreach(var i in list)
            {
                totalWeight += i.Weight;
            }

            var weight = Random.Range(0, totalWeight);
            var minWeight = 0;
            foreach(var i in list)
            {
                if(minWeight <= weight && weight < (minWeight + i.Weight))
                {
                    return i;
                }

                minWeight += i.Weight;
            }

            Assert.IsTrue(false, "抽選に失敗しました");

            return default(T);
        }
    }
}
