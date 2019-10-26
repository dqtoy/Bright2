using HK.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// プール可能なエフェクト
    /// </summary>
    public sealed class PoolableEffect : MonoBehaviour
    {
        private readonly static ObjectPoolBundle<PoolableEffect> pools = new ObjectPoolBundle<PoolableEffect>();

        private ObjectPool<PoolableEffect> pool;

        private float duration;

        void Update()
        {
            this.duration -= Time.deltaTime;
            if(this.duration <= 0.0f)
            {
                this.transform.SetParent(null);
                this.pool.Return(this);
            }
        }

        public PoolableEffect Rent(float delayReturnToPoolSeconds)
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;
            clone.duration = delayReturnToPoolSeconds;

            return clone;
        }
    }
}
