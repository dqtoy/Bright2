using HK.Bright2.GameSystems.Messages;
using HK.Bright2.StageControllers;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// Start関数でステージを生成するクラス
    /// </summary>
    public sealed class CreateStageOnStart : MonoBehaviour
    {
        [SerializeField]
        private Stage prefab = default;

        [SerializeField]
        private Vector2 initialPlayerPosition = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    x.Actor.Movement.Warp(_this.initialPlayerPosition);
                })
                .AddTo(this);
        }

        void Start()
        {
            Broker.Global.Publish(RequestChangeStage.Get(this.prefab));
        }
    }
}
