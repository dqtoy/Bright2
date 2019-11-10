using HK.Bright2.ActorControllers;
using HK.Bright2.CameraControllers;
using HK.Bright2.Database;
using HK.Bright2.StageControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ゲームの中核部分を担うクラス
    /// </summary>
    public sealed class GameSystem : MonoBehaviour
    {
        private static GameSystem instance;
        public static GameSystem Instance => instance;

        [SerializeField]
        private Cameraman cameraman = default;
        public Cameraman Cameraman => this.cameraman;

        [SerializeField]
        private MasterData masterData = default;
        public MasterData MasterData => this.masterData;

        public ActorManager ActorManager { get; private set; }

        public StageManager StageManager { get; private set; }

        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;

            this.ActorManager = new ActorManager();
            this.StageManager = new StageManager();
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;

            this.StageManager.Dispose();
        }
    }
}
