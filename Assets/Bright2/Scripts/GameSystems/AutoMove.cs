using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// s<see cref="Transform"/>を自動的に移動させるクラス
    /// </summary>
    public sealed class AutoMove : MonoBehaviour
    {
        [SerializeField]
        private Vector3 axis = default;

        [SerializeField]
        private float speed = default;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
        }

        void Update()
        {
            var vector =
                (this.cachedTransform.right * this.axis.x) +
                (this.cachedTransform.up * this.axis.y) +
                (this.cachedTransform.forward * this.axis.z);

            this.cachedTransform.localPosition += vector * this.speed * Time.deltaTime;
        }
    }
}
