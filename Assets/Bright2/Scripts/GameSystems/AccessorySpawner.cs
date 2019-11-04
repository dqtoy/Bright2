using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.GimmickControllers;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// アクセサリーを生成するクラス
    /// </summary>
    public sealed class AccessorySpawner : MonoBehaviour, IDiedActorGimmickSpawner<DropAccessory, AccessoryRecord>
    {
        [SerializeField]
        private Gimmick gimmickPrefab = default;

        [SerializeField]
        private Vector2 diedSpawnOffset = default;

        Gimmick IDiedActorGimmickSpawner<DropAccessory, AccessoryRecord>.GimmickPrefab => this.gimmickPrefab;

        void Awake()
        {
            this.ReceiveSpawnedActor(this.gameObject);
        }

        public IEnumerable<DropAccessory> GetDropData(Actor actor)
        {
            return actor.Context.DropItems.Accessories;
        }

        Vector3 IDiedActorGimmickSpawner<DropAccessory, AccessoryRecord>.GetSpawnPoint(Actor actor)
        {
            return actor.CachedTransform.position + new Vector3(this.diedSpawnOffset.x, this.diedSpawnOffset.y, 0.0f);
        }

        public void Setup(Gimmick gimmick, AccessoryRecord dropData)
        {
            foreach (var i in gimmick.GetComponentsInChildren<IAddAccessory>())
            {
                i.Setup(dropData);
            }
        }
    }
}
