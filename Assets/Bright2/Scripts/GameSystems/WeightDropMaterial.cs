using System;
using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using UnityEngine;

namespace HK.Bright2
{
    /// <summary>
    /// 重みで素材をドロップする暮らす
    /// </summary>
    [Serializable]
    public sealed class WeightDropMaterial
    {
        [SerializeField]
        private List<Element> items = default;

        public MaterialRecord Get
        {
            get
            {
                return this.items.Lottery().Item;
            }
        }

        [Serializable]
        public sealed class Element : IWeightLottery
        {
            [SerializeField]
            private MaterialRecord item = default;
            public MaterialRecord Item => this.item;

            [SerializeField]
            private int weight = default;

            int IWeightLottery.Weight => this.weight;
        }
    }
}
