using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 装備品マスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Equipment/List")]
    public sealed class EquipmentList : MasterDataList<EquipmentRecord>
    {
    }
}
