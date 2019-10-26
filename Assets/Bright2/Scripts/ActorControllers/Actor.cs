using UniRx;
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
        private ActorContext context = default;

        public ActorContext Context => this.context;

        public Transform CachedTransform { get; private set; }

        public ActorMovement Movement { get; private set; }

        public ActorStateManager StateManager { get; private set; }

        public ActorModelController ModelController { get; private set; }

        public ActorAnimationController AnimationController { get; private set; }

        public ActorInstanceStatusController StatusController { get; private set; }

        public readonly IMessageBroker Broker = new MessageBroker();

        void Awake()
        {
            this.CachedTransform = this.transform;
            Assert.IsNotNull(this.CachedTransform);

            this.Movement = this.GetComponent<ActorMovement>();
            Assert.IsNotNull(this.Movement);

            this.ModelController = this.GetComponent<ActorModelController>();
            Assert.IsNotNull(this.ModelController);

            this.AnimationController = new ActorAnimationController(this);

            this.StatusController = new ActorInstanceStatusController(this.context);

            this.StateManager = new ActorStateManager(this);
        }
    }
}
