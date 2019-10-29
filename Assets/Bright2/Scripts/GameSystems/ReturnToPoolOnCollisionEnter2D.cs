using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// <see cref="OnCollisionEnter2D(Collision2D)"/>のタイミングでプールするクラス
    /// </summary>
    public sealed class ReturnToPoolOnCollisionEnter2D : MonoBehaviour
    {
        private IPoolableComponent poolableComponent;

        void Awake()
        {
            this.poolableComponent = this.GetComponentInParent<IPoolableComponent>();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            this.poolableComponent.Return();
        }
    }
}
