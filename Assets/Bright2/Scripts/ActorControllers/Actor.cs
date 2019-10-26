using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// ゲーム中に何かしらのアクションを起こすオブジェクトの中核クラス
    /// </summary>
    public sealed class Actor : MonoBehaviour
    {
        [SerializeField]
        private ActorContext context;

        public ActorContext Context => this.context;
        
        public Transform CachedTransform { get; private set; }

        public ActorMovement Movement { get; private set; }

        public ActorStateManager StateManager { get; private set; }

        public ActorModelSwitcher ModelSwitcher { get; private set; }

        public ActorAnimationController AnimationController { get; private set; }

        void Awake()
        {
            this.CachedTransform = this.transform;
            Assert.IsNotNull(this.CachedTransform);

            this.Movement = this.GetComponent<ActorMovement>();
            Assert.IsNotNull(this.Movement);

            this.ModelSwitcher = this.GetComponent<ActorModelSwitcher>();
            Assert.IsNotNull(this.ModelSwitcher);

            this.AnimationController = new ActorAnimationController(this);

            this.StateManager = new ActorStateManager(this);
        }
    }
}
