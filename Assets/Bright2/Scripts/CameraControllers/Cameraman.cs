using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.CameraControllers
{
    /// <summary>
    /// カメラの状態を制御するクラス
    /// </summary>
    public sealed class Cameraman : MonoBehaviour
    {
        [SerializeField]
        private Camera controlledCamera = default;
        public Camera Camera => this.controlledCamera;

        public Transform CachedTransform { get; private set; }

        void Awake()
        {
            this.CachedTransform = this.transform;
        }
    }
}
