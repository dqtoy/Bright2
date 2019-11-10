using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.ItemModifiers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.WeaponControllers
{
    /// <summary>
    /// 武器のインスタンスデータ
    /// </summary>
    public sealed class InstanceWeapon
    {
        public readonly WeaponRecord WeaponRecord;

        public readonly List<IItemModifier> Modifiers = new List<IItemModifier>();

        public InstanceWeapon(WeaponRecord weaponRecord)
        {
            this.WeaponRecord = weaponRecord;
        }
    }
}
