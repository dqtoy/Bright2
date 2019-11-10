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
    /// 素材を生成するクラス
    /// </summary>
    public sealed class MaterialSpawner : MonoBehaviour, IDiedActorGimmickSpawner<DropMaterial, MaterialRecord>
    {
        [SerializeField]
        private Gimmick gimmickPrefab = default;

        [SerializeField]
        private Vector2 diedSpawnOffset = default;

        Gimmick IDiedActorGimmickSpawner<DropMaterial, MaterialRecord>.GimmickPrefab => this.gimmickPrefab;

        void Awake()
        {
            this.ReceiveSpawnedActor(this.gameObject);

            Broker.Global.Receive<RequestSpawnMaterial>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Setup(_this.CreateGimmick(x.Owner, x.SpawnPosition), x.MaterialRecord);
                })
                .AddTo(this);
        }

        Vector3 IDiedActorGimmickSpawner<DropMaterial, MaterialRecord>.GetSpawnPoint(Actor actor)
        {
            return actor.CachedTransform.position + new Vector3(this.diedSpawnOffset.x, this.diedSpawnOffset.y, 0.0f);
        }

        public IEnumerable<DropMaterial> GetDropData(Actor actor)
        {
            return actor.Context.DropItems.Materials;
        }

        public void Setup(Gimmick gimmick, MaterialRecord dropData)
        {
            foreach (var i in gimmick.GetComponentsInChildren<IAddMaterial>())
            {
                i.Setup(dropData);
            }
        }
    }
}
