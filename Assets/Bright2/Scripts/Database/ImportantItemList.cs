using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 大事なアイテムマスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/ImportantItem/List")]
    public sealed class ImportantItemList : MasterDataList<ImportantItemRecord>
    {
    }
}
