using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.DebugSystems
{
    /// <summary>
    /// デバッグでアイテムを追加するクラス
    /// </summary>
    public sealed class DebugAddItems : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    var masterData = GameSystem.Instance.MasterData;
                    foreach(var w in masterData.Weapon.Records)
                    {
                        x.Actor.StatusController.AddWeapon(w);
                    }

                    foreach(var a in masterData.Accessory.Records)
                    {
                        x.Actor.StatusController.AddAccessory(a);
                    }

                    foreach(var m in masterData.Material.Records)
                    {
                        x.Actor.StatusController.AddMaterial(m, 2);
                    }
                })
                .AddTo(this);
        }
    }
}
