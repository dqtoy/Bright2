using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// <see cref="Transform"/>を回転させるだけのクラス
    /// </summary>
    public sealed class AutoRotation : MonoBehaviour
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
            this.cachedTransform.localRotation *= Quaternion.AngleAxis(this.speed * Time.deltaTime, this.axis);
        }
    }
}
