using HK.Bright2.GameSystems.Messages;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
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

        /// <summary>
        /// ステージ切り替え中であるか
        /// </summary>
        private bool isChangingStage = false;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.target = x.Actor.CachedTransform;
                })
                .AddTo(this);

            this.LateUpdateAsObservable()
                .Where(_ => this.target != null)
                .Where(_ => !this.isChangingStage)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.cameraman.CachedTransform.position = _this.target.position;
                });

            Broker.Global.Receive<BeginChangeStage>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isChangingStage = true;
                })
                .AddTo(this);

            Broker.Global.Receive<EndChangeStage>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isChangingStage = false;
                })
                .AddTo(this);
        }
    }
}
