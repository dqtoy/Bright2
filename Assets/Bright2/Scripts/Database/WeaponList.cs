using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 武器マスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Weapon/List")]
    public sealed class WeaponList : MasterDataList<WeaponRecord>
    {
    }
}
