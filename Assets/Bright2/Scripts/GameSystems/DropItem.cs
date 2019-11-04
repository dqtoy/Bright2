using System;
using HK.Bright2.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// ドロップアイテムの抽象クラス
    /// </summary>
    [Serializable]
    public abstract class DropItem<T> : IDropItem<T>
    {
        [SerializeField]
        private T item = default;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float winningRate = default;

        public T Get => this.item;

        public float WinningRate => this.winningRate;

        public bool IsWinning => this.Lottery();
    }
}
