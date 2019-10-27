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

        public PlayerManager PlayerManager { get; private set; }

        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;

            this.PlayerManager = new PlayerManager();
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;
        }
    }
}
