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
        private EquipmentList equipment = default;
        public EquipmentList Equipment => this.equipment;
    }
}
