using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アイテム修飾のレシピマスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/ItemModifierRecipe/List")]
    public sealed class ItemModifierRecipeList : MasterDataList<ItemModifierRecipeRecord>
    {
    }
}
