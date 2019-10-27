using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ゲームの中核部分を担うクラス
    /// </summary>
    public sealed class GameSystem : MonoBehaviour
    {
        public PlayerManager PlayerManager { get; private set; }

        void Awake()
        {
            this.PlayerManager = new PlayerManager();
        }
    }
}
