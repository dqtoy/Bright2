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
    /// 武器を生成するクラス
    /// </summary>
    public sealed class WeaponSpawner : MonoBehaviour, IDiedActorGimmickSpawner<DropWeapon, WeaponRecord>
    {
        [SerializeField]
        private Gimmick gimmickPrefab = default;

        [SerializeField]
        private Vector2 diedSpawnOffset = default;

        Gimmick IDiedActorGimmickSpawner<DropWeapon, WeaponRecord>.GimmickPrefab => this.gimmickPrefab;

        void Awake()
        {
            this.ReceiveSpawnedActor(this.gameObject);

            Broker.Global.Receive<RequestSpawnWeapon>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Setup(_this.CreateGimmick(x.Owner, x.SpawnPosition), x.WeaponRecord);
                })
                .AddTo(this);
        }

        Vector3 IDiedActorGimmickSpawner<DropWeapon, WeaponRecord>.GetSpawnPoint(Actor actor)
        {
            return actor.CachedTransform.position + new Vector3(this.diedSpawnOffset.x, this.diedSpawnOffset.y, 0.0f);
        }

        public IEnumerable<DropWeapon> GetDropData(Actor actor)
        {
            return actor.Context.DropItems.Weapons;
        }

        public void Setup(Gimmick gimmick, WeaponRecord dropData)
        {
            foreach (var i in gimmick.GetComponentsInChildren<IAddWeapon>())
            {
                i.Setup(dropData);
            }
        }

        bool IDiedActorGimmickSpawner<DropWeapon, WeaponRecord>.Lottery(DropWeapon dropItem, Actor attacker)
        {
            return dropItem.Lottery(attacker.StatusController.ItemModifierEffect.GetPercent(Constants.ItemModifierType.DropWeaponAndAccessoryRate));
        }
    }
}
