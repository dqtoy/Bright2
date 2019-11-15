using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// ゲームで利用するマスターデータをまとめたクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/MasterData/MasterData")]
    public sealed class MasterData :ScriptableObject
    {
        [SerializeField]
        private WeaponList weapon = default;
        public WeaponList Weapon => this.weapon;

        [SerializeField]
        private AccessoryList accessory = default;
        public AccessoryList Accessory => this.accessory;

        [SerializeField]
        private MaterialList material = default;
        public MaterialList Material => this.material;

        [SerializeField]
        private ItemModifierRecipeList itemModifierRecipe = default;
        public ItemModifierRecipeList ItemModifierRecipe => this.itemModifierRecipe;

        [SerializeField]
        private WeaponRecipeList weaponRecipeList = default;
        public WeaponRecipeList WeaponRecipeList => this.weaponRecipeList;
    }
}
