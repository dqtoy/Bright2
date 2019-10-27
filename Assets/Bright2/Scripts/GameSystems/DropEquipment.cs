using System;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// 装備品のドロップデータ
    /// </summary>
    [Serializable]
    public sealed class DropEquipment : IDropItem<EquipmentRecord>
    {
        [SerializeField]
        private EquipmentRecord record = default;

        [SerializeField][Range(0.0f, 1.0f)]
        private float winningRate = default;

        public EquipmentRecord Get => this.record;

        public float WinningRate => this.winningRate;

        bool IsWinning => this.Lottery();

        bool ILottery.IsWinning => this.IsWinning;
    }
}
