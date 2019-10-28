using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="IAddEquipment.Setup(EquipmentRecord)"/>のタイミングでスプライトを設定するクラス
    /// </summary>
    public sealed class SetSpriteOnAddEquipment : MonoBehaviour, IAddEquipment
    {
        [SerializeField]
        private SpriteRenderer controlledRenderer = default;

        public void Setup(EquipmentRecord equipment)
        {
            this.controlledRenderer.sprite = equipment.Icon;
        }
    }
}
