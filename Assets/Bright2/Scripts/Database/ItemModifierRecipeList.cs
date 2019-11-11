using System.Collections.Generic;
using HK.Bright2.MaterialControllers;
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
        public IReadOnlyList<ItemModifierRecipeRecord> GetViewableRecipes(IReadOnlyDictionary<MaterialRecord, InstanceMaterial> possessionMaterials)
        {
            var result = new List<ItemModifierRecipeRecord>();

            foreach(var r in this.Records)
            {
                if(!r.NeedMaterials.IsViewableList(possessionMaterials))
                {
                    continue;
                }

                result.Add(r);
            }

            return result;
        }
    }
}
