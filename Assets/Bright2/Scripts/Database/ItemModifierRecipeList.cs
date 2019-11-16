using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.ItemControllers;
using UnityEngine;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アイテム修飾のレシピマスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/ItemModifierRecipe/List")]
    public sealed class ItemModifierRecipeList : MasterDataList<ItemModifierRecipeRecord>
    {
        public IReadOnlyList<ItemModifierRecipeRecord> GetViewableRecipes(Inventory inventory)
        {
            var result = new List<ItemModifierRecipeRecord>();

            foreach(var r in this.Records)
            {
                if(!r.NeedItems.IsViewableList(inventory))
                {
                    continue;
                }

                result.Add(r);
            }

            return result;
        }
    }
}
