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
    public sealed class AccessorySpawner : DiedActorGimmickSpawner<DropAccessory, AccessoryRecord>
    {
        protected override IEnumerable<DropAccessory> GetDropData(Actor actor) => actor.Context.BasicStatus.DropAccessories;

        protected override void Setup(Gimmick gimmick, AccessoryRecord dropData)
        {
            foreach(var i in gimmick.GetComponentsInChildren<IAddAccessory>())
            {
                i.Setup(dropData);
            }
        }
    }
}
