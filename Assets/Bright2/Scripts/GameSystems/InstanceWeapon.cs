using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// 武器のインスタンスデータ
    /// </summary>
    public sealed class InstanceWeapon
    {
        public readonly WeaponRecord WeaponRecord;

        public InstanceWeapon(WeaponRecord weaponRecord)
        {
            this.WeaponRecord = weaponRecord;
        }
    }
}
