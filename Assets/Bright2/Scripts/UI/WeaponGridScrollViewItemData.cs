using System.Collections.Generic;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WeaponGridScrollViewItemData
    {
        public List<WeaponRecord> Records { get; }

        public WeaponGridScrollViewItemData()
        {
            this.Records = new List<WeaponRecord>();
        }

        public bool CanAddRecord => this.Records.Count < WeaponGridScrollViewCell.ElementMax;
    }
}
