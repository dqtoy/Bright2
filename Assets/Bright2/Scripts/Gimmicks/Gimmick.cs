using HK.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers
{
    /// <summary>
    /// <see cref="Actor"/>が生成するギミックの中核となるクラス
    /// </summary>
    public sealed class Gimmick : MonoBehaviour
    {
        private readonly static ObjectPoolBundle<Gimmick> pools = new ObjectPoolBundle<Gimmick>();

        private ObjectPool<Gimmick> pool;

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
    }
}
