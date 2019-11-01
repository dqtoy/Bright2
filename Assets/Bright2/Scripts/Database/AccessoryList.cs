using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アクセサリーマスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Accessory/List")]
    public sealed class AccessoryList : MasterDataList<AccessoryRecord>
    {
    }
}
