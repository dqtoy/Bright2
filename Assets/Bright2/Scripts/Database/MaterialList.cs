using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 素材マスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/Material/List")]
    public sealed class MaterialList : MasterDataList<MaterialRecord>
    {
    }
}
