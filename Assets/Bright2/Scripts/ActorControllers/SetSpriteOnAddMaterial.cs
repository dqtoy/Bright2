using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="IAddMaterial.Setup(MaterialRecord)"/>のタイミングでスプライトを設定するクラス
    /// </summary>
    public sealed class SetSpriteOnAddMaterial : MonoBehaviour, IAddMaterial
    {
        [SerializeField]
        private SpriteRenderer controlledRenderer = default;

        public void Setup(MaterialRecord materialRecord)
        {
            this.controlledRenderer.sprite = materialRecord.Icon;
        }
    }
}
