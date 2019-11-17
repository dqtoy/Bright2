using UnityEngine;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アイテム修飾のレシピマスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/ItemModifierRecipe/List")]
    public sealed class ItemModifierRecipeList : MasterDataList<ItemModifierRecipeRecord>, IMasterDataListRecipe<ItemModifierRecipeRecord>
    {
    }
}
