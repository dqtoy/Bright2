using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="IAddImportantItem.Setup(ImportantItemRecord)"/>のタイミングでスプライトを設定するクラス
    /// </summary>
    public sealed class SetSpriteOnAddImportantItem : MonoBehaviour, IAddImportantItem
    {
        [SerializeField]
        private SpriteRenderer controlledRenderer = default;

        public void Setup(ImportantItemRecord importantItem)
        {
            this.controlledRenderer.sprite = importantItem.Icon;
        }
    }
}
