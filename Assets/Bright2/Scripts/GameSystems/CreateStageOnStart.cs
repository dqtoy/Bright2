using HK.Bright2.GameSystems.Messages;
using HK.Bright2.StageControllers;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// Start関数でステージを生成するクラス
    /// </summary>
    public sealed class CreateStageOnStart : MonoBehaviour
    {
        [SerializeField]
        private Stage prefab = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    x.Actor.Movement.Warp(_this.prefab.StartPoint.position);
                })
                .AddTo(this);
        }

        void Start()
        {
            Broker.Global.Publish(RequestChangeStage.Get(this.prefab));
        }

#if UNITY_EDITOR
        public void SetPrefab(Stage prefab)
        {
            this.prefab = prefab;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
