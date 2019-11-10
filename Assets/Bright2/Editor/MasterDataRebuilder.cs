using System.Linq;
using HK.Bright2.Database;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Editor
{
    /// <summary>
    /// マスターデータを再構成するクラス
    /// </summary>
    public sealed class MasterDataRebuilder
    {
        [MenuItem("HK/Bright2/MasterData/Rebuild")]
        private static void Rebuild()
        {
            ResetList<WeaponList, WeaponRecord>("Assets/Bright2/Database/MasterData/Lists/Weapon.asset", "Assets/Bright2/Database/MasterData/Weapons");
            ResetList<AccessoryList, AccessoryRecord>("Assets/Bright2/Database/MasterData/Lists/Accessory.asset", "Assets/Bright2/Database/MasterData/Accessories");
            ResetList<MaterialList, MaterialRecord>("Assets/Bright2/Database/MasterData/Lists/Material.asset", "Assets/Bright2/Database/MasterData/Materials");
            ResetList<ItemModifierRecipeList, ItemModifierRecipeRecord>("Assets/Bright2/Database/MasterData/Lists/ItemModifierRecipe.asset", "Assets/Bright2/Database/MasterData/ItemModifierRecipes");
        }

        private static void ResetList<L, E>(string listPath, string recordsPath)
            where L : MasterDataList<E>
            where E : MasterDataRecord
        {
            var list = AssetDatabase.LoadAssetAtPath<L>(listPath);
            var newElements = AssetDatabase
                .FindAssets("", new string[] { recordsPath })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<E>);
            list.EditableList.Clear();
            list.EditableList.AddRange(newElements);
            EditorUtility.SetDirty(list);
        }
    }
}
