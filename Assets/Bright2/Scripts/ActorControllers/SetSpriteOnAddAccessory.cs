using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="IAddAccessory.Setup(AccessoryRecord)"/>のタイミングでスプライトを設定するクラス
    /// </summary>
    public sealed class SetSpriteOnAddAccessory : MonoBehaviour, IAddAccessory
    {
        [SerializeField]
        private SpriteRenderer controlledRenderer = default;

        public void Setup(AccessoryRecord accessory)
        {
            this.controlledRenderer.sprite = accessory.Icon;
        }
    }
}
