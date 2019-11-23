using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.StageControllers.Messages;
using Prime31;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Transform"/>の物理移動処理を制御するクラス
    /// </summary>
    public sealed class TransformMovement : MonoBehaviour
    {
        [SerializeField]
        private Vector2 gravity = default;

        private Transform controlledTransform;

        private IBroker brokableObject;

        private Vector2 velocity;

        private Vector2 currentGravity;

        private CharacterController2D characterController;

        /// <summary>
        /// 麻痺中であるか
        /// </summary>
        private bool isParalysis = false;

        /// <summary>
        /// 混乱中であるか
        /// </summary>
        private bool isConfuse = false;

        /// <summary>
        /// 水中に入っているか
        /// </summary>
        private bool isEnterUnderWater = false;

        /// <summary>
        /// Yの重力の最大値
        /// </summary>
        private const float GravityYMax = -26.0f;

        public bool IgnoreOneWayPlatformsThisFrame
        {
            get
            {
                return this.characterController.ignoreOneWayPlatformsThisFrame;
            }
            set
            {
                this.characterController.ignoreOneWayPlatformsThisFrame = value;
            }
        }

        public bool IsGrounded
        {
            get
            {
                return this.characterController.isGrounded;
            }
        }

        void Awake()
        {
            this.controlledTransform = this.transform;
            this.brokableObject = this.GetComponent<IBroker>();

            this.brokableObject?.Broker.Receive<AttachedAbnormalCondition>()
                .Where(x => x.Type == Constants.AbnormalStatus.Paralysis)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isParalysis = true;
                })
                .AddTo(this);

            this.brokableObject?.Broker.Receive<DetachedAbnormalCondition>()
                .Where(x => x.Type == Constants.AbnormalStatus.Paralysis)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isParalysis = false;
                })
                .AddTo(this);

            this.brokableObject?.Broker.Receive<AttachedAbnormalCondition>()
                .Where(x => x.Type == Constants.AbnormalStatus.Confuse)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isConfuse = true;
                })
                .AddTo(this);

            this.brokableObject?.Broker.Receive<DetachedAbnormalCondition>()
                .Where(x => x.Type == Constants.AbnormalStatus.Confuse)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isConfuse = false;
                })
                .AddTo(this);

            this.brokableObject?.Broker.Receive<EnterUnderWater>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isEnterUnderWater = true;
                    Debug.Log("Enter Underwater");
                })
                .AddTo(this);

            this.brokableObject?.Broker.Receive<ExitUnderWater>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.isEnterUnderWater = false;
                })
                .AddTo(this);

            this.characterController = this.GetComponent<CharacterController2D>();
            Assert.IsNotNull(this.characterController);
        }

        private bool canPublishLanded = true;

        void Update()
        {
            this.AddGravity();
            this.UnderWaterProccess();

            var oldIsGrouded = this.characterController.isGrounded;

            this.characterController.move(this.velocity);

            var isGrounded = this.characterController.isGrounded;

            if(!oldIsGrouded && isGrounded)
            {
                this.gravity.x = 0.0f;
                this.currentGravity.x = 0.0f;
            }

            if(!isGrounded && this.velocity.y < 0.0f)
            {
                this.brokableObject?.Broker.Publish(Fall.Get());
            }

            if(this.canPublishLanded)
            {
                if(isGrounded)
                {
                    this.brokableObject?.Broker.Publish(Landed.Get());
                    this.canPublishLanded = false;
                }
            }
            else
            {
                if(!isGrounded)
                {
                    this.canPublishLanded = true;
                }
            }

            if(isGrounded)
            {
                this.currentGravity.y = 0.0f;
            }
            if(this.characterController.collisionState.above)
            {
                this.currentGravity.y = 0.0f;
            }

            if(this.velocity.x == 0.0f)
            {
                this.brokableObject?.Broker.Publish(Idle.Get());
            }
            else
            {
                this.brokableObject?.Broker.Publish(Move.Get(this.velocity));
            }
            this.velocity = Vector2.zero;
        }

        public void AddMove(Vector2 velocity)
        {
            if(this.isParalysis)
            {
                return;
            }

            if(this.isConfuse)
            {
                velocity.x *= -1.0f;
            }

            this.velocity += velocity;
        }

        public void SetGravity(Vector2 gravity)
        {
            this.currentGravity = gravity;
        }

        public void Warp(Vector2 position)
        {
            this.velocity = Vector2.zero;
            this.currentGravity = Vector2.zero;
            this.controlledTransform.position = position;
        }

        private void AddGravity()
        {
            var gravity = this.gravity;
            if(this.isEnterUnderWater)
            {
                gravity *= 0.5f;
            }
            
            this.currentGravity += gravity * Time.deltaTime;
            this.currentGravity.y = Mathf.Max(this.currentGravity.y, GravityYMax);
            this.velocity += this.currentGravity * Time.deltaTime;
        }

        private void UnderWaterProccess()
        {
            if(!this.isEnterUnderWater)
            {
                return;
            }

            this.velocity *= 0.5f;
        }
    }
}
