using UnityEngine;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アクセサリーのレシピマスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/AccessoryRecipe/List")]
    public sealed class AccessoryRecipeList : MasterDataList<AccessoryRecipeRecord>, IMasterDataListRecipe<AccessoryRecipeRecord>
    {
    }
}
