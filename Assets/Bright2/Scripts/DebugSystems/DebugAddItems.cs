using System.Collections.Generic;
using HK.Bright2.Database;
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
        [SerializeField]
        private List<WeaponRecord> possessionWeapons = default;

        [SerializeField]
        private List<AccessoryRecord> possessionAccessories = default;

        [SerializeField]
        private List<int> equippedAccessories = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    foreach(var w in _this.possessionWeapons)
                    {
                        x.Actor.StatusController.AddWeapon(w);
                    }

                    foreach(var a in _this.possessionAccessories)
                    {
                        x.Actor.StatusController.AddAccessory(a);
                    }

                    for (var i = 0; i < _this.equippedAccessories.Count; i++)
                    {
                        x.Actor.StatusController.ChangeEquippedAccessory(i, _this.equippedAccessories[i]);
                    }
                })
                .AddTo(this);
        }
    }
}
