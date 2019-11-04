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
    /// <see cref="Actor"/>が死亡した際にギミックを生成するクラス
    /// </summary>
    public abstract class DiedActorGimmickSpawner<T, E> : MonoBehaviour where T : IDropItem<E>
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
                    foreach (var d in this.GetDropData(a))
                    {
                        if (!d.Lottery())
                        {
                            continue;
                        }

                        _this.Setup(_this.CreateGimmick(a), d.Get);
                    }
                })
                .AddTo(actor);
        }

        private Gimmick CreateGimmick(Actor actor)
        {
            var gimmick = this.prefab.Rent();
            gimmick.transform.position = actor.CachedTransform.position + this.offset;
            gimmick.transform.rotation = Quaternion.identity;
            gimmick.Activate(actor);

            return gimmick;
        }

        protected abstract IEnumerable<T> GetDropData(Actor actor);

        protected abstract void Setup(Gimmick gimmick, E dropData);
    }
}
