using System;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine;

namespace HK.Bright2.ItemControllers
{
    /// <summary>
    /// 必要なアイテムを持つクラス
    /// </summary>
    [Serializable]
    public sealed class NeedItems
    {
        [SerializeField]
        private Element[] elements = default;
        public Element[] Elements => this.elements;

        public bool IsEnough(Inventory inventory)
        {
            foreach(var e in this.elements)
            {
                if(!inventory.IsEnough(e.MasterDataRecord, e.Amount))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// リストなどのUIに表示可能か返す
        /// </summary>
        public bool IsViewableList(Inventory inventory)
        {
            foreach(var e in this.elements)
            {
                if(inventory.Contains(e.MasterDataRecord))
                {
                    return true;
                }
            }

            return false;
        }

        [Serializable]
        public sealed class Element
        {
            [SerializeField]
            private MasterDataRecord masterDataRecord = default;
            public MasterDataRecord MasterDataRecord => this.masterDataRecord;

            [SerializeField]
            private int amount = default;
            public int Amount => this.amount;
        }
    }
}
