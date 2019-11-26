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
    public sealed class ImportantItemSpawner : MonoBehaviour, IDiedActorGimmickSpawner<DropImportantItem, ImportantItemRecord>
    {
        [SerializeField]
        private Gimmick gimmickPrefab = default;

        [SerializeField]
        private Vector2 diedSpawnOffset = default;

        Gimmick IDiedActorGimmickSpawner<DropImportantItem, ImportantItemRecord>.GimmickPrefab => this.gimmickPrefab;

        void Awake()
        {
            this.ReceiveSpawnedActor(this.gameObject);

            Broker.Global.Receive<RequestSpawnImportantItem>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Setup(_this.CreateGimmick(x.Owner, x.SpawnPosition), x.ImportantItemRecord);
                })
                .AddTo(this);
        }

        public IEnumerable<DropImportantItem> GetDropData(Actor actor)
        {
            return actor.Context.DropItems.ImportantItems;
        }

        Vector3 IDiedActorGimmickSpawner<DropImportantItem, ImportantItemRecord>.GetSpawnPoint(Actor actor)
        {
            return actor.CachedTransform.position + new Vector3(this.diedSpawnOffset.x, this.diedSpawnOffset.y, 0.0f);
        }

        public void Setup(Gimmick gimmick, ImportantItemRecord dropData)
        {
            foreach (var i in gimmick.GetComponentsInChildren<IAddImportantItem>())
            {
                i.Setup(dropData);
            }
        }

        bool IDiedActorGimmickSpawner<DropImportantItem, ImportantItemRecord>.Lottery(DropImportantItem dropItem, Actor attacker)
        {
            return dropItem.Lottery(attacker.StatusController.ItemModifierEffect.GetPercent(Constants.ItemModifierType.DropEquipmentUpRate));
        }
    }
}
