using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.GimmickControllers;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="IDiedActorGimmickSpawner"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        public static void ReceiveSpawnedActor<T, E>(this IDiedActorGimmickSpawner<T, E> self, GameObject owner)
            where T : IDropItem<E>
        {
            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState2(self, owner, (x, _this, _owner) =>
                {
                    _this.ReceiveDied(x.Actor, _owner);
                })
                .AddTo(owner);
        }

        private static void ReceiveDied<T, E>(this IDiedActorGimmickSpawner<T, E> self, Actor actor, GameObject owner)
            where T : IDropItem<E>
        {
            actor.Broker.Receive<Died>()
                .SubscribeWithState2(self, actor, (x, _this, a) =>
                {
                    foreach (var d in _this.GetDropData(a))
                    {
                        if (!_this.Lottery(d, x.Attacker))
                        {
                            continue;
                        }

                        _this.Setup(_this.CreateGimmick(a, _this.GetSpawnPoint(a)), d.Get);
                    }
                })
                .AddTo(actor);
        }

        public static Gimmick CreateGimmick<T, E>(this IDiedActorGimmickSpawner<T, E> self, Actor actor, Vector3 position)
            where T : IDropItem<E>
        {
            var gimmick = self.GimmickPrefab.Rent();
            gimmick.transform.position = position;
            gimmick.transform.rotation = Quaternion.identity;
            gimmick.Activate(actor);

            return gimmick;
        }
    }
}
