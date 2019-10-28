using System.Collections.Generic;
using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HUDWeaponGridScrollViewItemData
    {
        public List<WeaponRecord> Records { get; }

        public HUDWeaponGridScrollViewItemData(List<WeaponRecord> records)
        {
            this.Records = records;
        }
    }
}
