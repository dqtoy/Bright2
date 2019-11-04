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
    public sealed class WeaponSpawner : DiedActorGimmickSpawner<DropWeapon, WeaponRecord>
    {
        protected override IEnumerable<DropWeapon> GetDropData(Actor actor) => actor.Context.DropItems.Weapons;

        protected override void Awake()
        {
            base.Awake();

            Broker.Global.Receive<RequestSpawnWeapon>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Setup(_this.CreateGimmick(x.Owner, x.SpawnPosition), x.WeaponRecord);
                })
                .AddTo(this);
        }

        protected override void Setup(Gimmick gimmick, WeaponRecord dropData)
        {
            foreach(var i in gimmick.GetComponentsInChildren<IAddWeapon>())
            {
                i.Setup(dropData);
            }
        }
    }
}
