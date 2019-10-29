using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// <see cref="OnTriggerEnter2D(Collider2D)"/>のタイミングでプールするクラス
    /// </summary>
    public sealed class ReturnToPoolOnTriggerEnter2D : MonoBehaviour
    {
        private IPoolableComponent poolableComponent;

        void Awake()
        {
            this.poolableComponent = this.GetComponentInParent<IPoolableComponent>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            this.poolableComponent.Return();
        }
    }
}
