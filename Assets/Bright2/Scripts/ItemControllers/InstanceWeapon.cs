using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.ItemModifiers;
using HK.Bright2.UIControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ItemControllers
{
    /// <summary>
    /// 武器のインスタンスデータ
    /// </summary>
    public sealed class InstanceWeapon : IIconHolder, INameHolder, IViewableList
    {
        public readonly WeaponRecord WeaponRecord;

        public readonly List<IItemModifier> Modifiers = new List<IItemModifier>();

        public InstanceWeapon(WeaponRecord weaponRecord)
        {
            this.WeaponRecord = weaponRecord;
        }

        Sprite IIconHolder.Icon => this.WeaponRecord.Icon;

        string INameHolder.Name => this.WeaponRecord.WeaponName;
    }
}
