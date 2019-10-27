using HK.Bright2.StageControllers;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
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
        
        void Start()
        {
            Broker.Global.Publish(RequestChangeStage.Get(this.prefab));
        }
    }
}
