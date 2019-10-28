using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="IAddWeapon.Setup(WeaponRecord)"/>のタイミングでスプライトを設定するクラス
    /// </summary>
    public sealed class SetSpriteOnAddEquipment : MonoBehaviour, IAddWeapon
    {
        [SerializeField]
        private SpriteRenderer controlledRenderer = default;

        public void Setup(WeaponRecord equipment)
        {
            this.controlledRenderer.sprite = equipment.Icon;
        }
    }
}
