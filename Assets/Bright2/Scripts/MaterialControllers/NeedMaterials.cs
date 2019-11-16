using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.MaterialControllers
{
    /// <summary>
    /// 必要な素材を持つクラス
    /// </summary>
    [Serializable]
    public sealed class NeedMaterials
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
                if(inventory.IsEnough(e.MasterDataRecord, e.Amount))
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
