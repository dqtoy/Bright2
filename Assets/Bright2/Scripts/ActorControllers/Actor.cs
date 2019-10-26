using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// ゲーム中に何かしらのアクションを起こすオブジェクトの中核クラス
    /// </summary>
    public sealed class Actor : MonoBehaviour
    {
        public Transform CachedTransform { get; private set; }

        public ActorMovement Movement { get; private set; }

        public ActorStateManager StateManager{ get; private set; }

        void Awake()
        {
            this.CachedTransform = this.transform;
            Assert.IsNotNull(this.CachedTransform);

            this.Movement = this.GetComponent<ActorMovement>();
            Assert.IsNotNull(this.Movement);

            this.StateManager = new ActorStateManager(this);
        }
    }
}
