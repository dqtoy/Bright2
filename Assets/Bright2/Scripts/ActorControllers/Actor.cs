using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// ゲーム中に何かしらのアクションを起こすオブジェクトの中核クラス
    /// </summary>
    public sealed class Actor : MonoBehaviour, IBroker
    {
        [SerializeField]
        private ActorContext context = default;

        public ActorContext Context => this.context;

        public Transform CachedTransform { get; private set; }

        public TransformMovement Movement { get; private set; }

        public ActorStateManager StateManager { get; private set; }

        public ActorModelController ModelController { get; private set; }

        public ActorAnimationController AnimationController { get; private set; }

        public ActorInstanceStatusController StatusController { get; private set; }

        public ActorTransformHolder TransformHolder { get; private set; }

        public ActorLifeCycleController LifeCycleController { get; private set; }

        public readonly IMessageBroker Broker = new MessageBroker();

        IMessageBroker IBroker.Broker => this.Broker;

        void Awake()
        {
            this.CachedTransform = this.transform;
            Assert.IsNotNull(this.CachedTransform);

            this.Movement = this.GetComponent<TransformMovement>();
            Assert.IsNotNull(this.Movement);

            this.ModelController = this.GetComponent<ActorModelController>();
            Assert.IsNotNull(this.ModelController);

            this.TransformHolder = this.GetComponent<ActorTransformHolder>();
            Assert.IsNotNull(this.TransformHolder);

            this.AnimationController = new ActorAnimationController(this);

            this.StatusController = new ActorInstanceStatusController(this, this.context);

            this.LifeCycleController = new ActorLifeCycleController(this);

            this.StateManager = new ActorStateManager(this);
        }
    }
}
