using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.CameraControllers
{
    /// <summary>
    /// ゲーム中に利用するカメラを制御するクラス
    /// </summary>
    public sealed class GameCameraController : MonoBehaviour
    {
        [SerializeField]
        private Cameraman cameraman = default;

        [SerializeField]
        private Transform target = default;

        void Awake()
        {
            this.LateUpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cameraman.CachedTransform.position = _this.target.position;
                });
        }
    }
}
