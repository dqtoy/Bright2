using System.Collections.Generic;
using HK.Bright2.MaterialControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// アイテム修飾のレシピマスターデータのリスト
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/WeaponRecipe/List")]
    public sealed class WeaponRecipeList : MasterDataList<WeaponRecipeRecord>
    {
    }
}
