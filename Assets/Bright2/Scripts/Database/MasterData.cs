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
    }
}
