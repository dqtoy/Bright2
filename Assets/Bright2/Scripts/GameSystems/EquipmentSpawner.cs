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

namespace HK.Bright2
{
    /// <summary>
    /// 装備品を生成するクラス
    /// </summary>
    public sealed class EquipmentSpawner : MonoBehaviour
    {
        [SerializeField]
        private Gimmick prefab = default;

        [SerializeField]
        private Vector3 offset = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveActor(x.Actor);
                })
                .AddTo(this);
        }

        private void ObserveActor(Actor actor)
        {
            actor.Broker.Receive<Died>()
                .SubscribeWithState2(this, actor, (x, _this, a) =>
                {
                    foreach(var d in a.Context.BasicStatus.DropEquipments)
                    {
                        if(!d.Lottery())
                        {
                            continue;
                        }

                        _this.CreateGimmick(a, d.Get);
                    }
                })
                .AddTo(actor);
        }

        private void CreateGimmick(Actor actor, EquipmentRecord equipment)
        {
            var gimmick = this.prefab.Rent();
            gimmick.transform.position = actor.CachedTransform.position + this.offset;
            gimmick.Activate(actor);
            
            foreach(var i in gimmick.GetComponentsInChildren<IAddEquipment>())
            {
                i.Setup(equipment);
            }
        }
    }
}
