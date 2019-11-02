using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GimmickControllers.Decorators;
using HK.Bright2.GimmickControllers.Messages;
using HK.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers
{
    /// <summary>
    /// <see cref="Actor"/>が生成するギミックの中核となるクラス
    /// </summary>
    public sealed class Gimmick : MonoBehaviour, IPoolableComponent
    {
        public readonly IMessageBroker Broker = new MessageBroker();

        public Constants.Direction Direction { get; private set; }

        private readonly static ObjectPoolBundle<Gimmick> pools = new ObjectPoolBundle<Gimmick>();

        private ObjectPool<Gimmick> pool;

        private IGimmickDecorator[] decotators = null;

        private GimmickLifeCycleController lifeCycleController;

        private List<IAffectedSpeedUp> affectedSpeedUps;

        private bool isPooled = true;

        void Awake()
        {
            this.lifeCycleController = new GimmickLifeCycleController(this);
            this.affectedSpeedUps = new List<IAffectedSpeedUp>(this.GetComponentsInChildren<IAffectedSpeedUp>());
            this.affectedSpeedUps.Add(new DOTweenAnimationSetTimeScale(this.gameObject));
        }

        public Gimmick Rent()
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();

            Assert.IsTrue(clone.isPooled, $"{clone.name}はプールされていないのにRentされました");
            clone.pool = pool;
            clone.isPooled = false;

            return clone;
        }

        public void Return()
        {
            if(this.isPooled)
            {
                return;
            }
            this.pool.Return(this);
            this.transform.SetParent(null);

            Assert.IsFalse(this.isPooled, $"{this.name}はプールされているのにReturnされました");
            this.isPooled = true;
        }

        public void Activate(Actor owner)
        {
            this.Direction = owner.StatusController.Direction;
            foreach (var a in this.affectedSpeedUps)
            {
                a.Affected(owner.StatusController.AccessoryEffect.FireSpeedUp);
            }

            this.decotators = this.decotators ?? this.GetComponentsInChildren<IGimmickDecorator>();
            foreach(var d in this.decotators)
            {
                d.OnActivate(this, owner);
            }
        }
    }
}
