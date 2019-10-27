using HK.Bright2.ActorControllers;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// <see cref="ActorSpawnerOnStart.Start"/>関数のタイミングで<see cref="Actor"/>を生成するクラス
    /// </summary>
    public sealed class ActorSpawnerOnStart : MonoBehaviour
    {
        [SerializeField]
        private Actor prefab = default;
        
        void Start()
        {
            prefab.Spawn(this.transform.position);
        }
    }
}
