using HK.Bright2.StageControllers.Messages;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="EnterUnderWater"/>のタイミングで重力を設定するクラス
    /// </summary>
    public sealed class SetGravityEnterUnderWater : MonoBehaviour
    {
        [SerializeField]
        private Vector2 gravity = default;
        
        private Actor owner;

        void Awake()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);

            this.owner.Broker.Receive<EnterUnderWater>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.Movement.PushGravity(_this.gravity);
                    Debug.Log("?");
                })
                .AddTo(this);

            this.owner.Broker.Receive<ExitUnderWater>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.Movement.PopGravity();
                })
                .AddTo(this);
        }
    }
}
