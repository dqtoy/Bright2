using UnityEngine;
using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using HK.Bright2.GameSystems.Messages;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="Actor"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// <paramref name="prefab"/>を生成する
        /// </summary>
        public static Actor Spawn(this Actor prefab, Vector3 position)
        {
            var instance = Object.Instantiate(prefab);
            instance.CachedTransform.position = position;

            Broker.Global.Publish(SpawnedActor.Get(instance));

            return instance;
        }
    }
}
