using HK.Bright2.ActorControllers;
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
    public sealed class Gimmick : MonoBehaviour
    {
        public readonly IMessageBroker Broker = new MessageBroker();
        
        private readonly static ObjectPoolBundle<Gimmick> pools = new ObjectPoolBundle<Gimmick>();

        private ObjectPool<Gimmick> pool;

        private IGimmickDecorator[] decotators = null;

        public Gimmick Rent()
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;

            return clone;
        }

        public void Return()
        {
            this.pool.Return(this);
        }

        public void Activate(Actor owner)
        {
            this.decotators = this.decotators ?? this.GetComponentsInChildren<IGimmickDecorator>();

            this.Broker.Publish(ActivateGimmick.Get(owner));
        }
    }
}
