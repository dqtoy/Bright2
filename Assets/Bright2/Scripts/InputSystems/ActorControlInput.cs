using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Framework.EventSystems;
using HK.Bright2.GameSystems.Messages;
using UniRx;

namespace HK.Bright2.InputSystems
{
    /// <summary>
    /// <see cref="Actor"/>を操作する入力を制御するクラス
    /// </summary>
    public sealed class ActorControlInput : MonoBehaviour
    {
        [SerializeField]
        private Actor actor = default;

        [SerializeField]
        private EquipmentRecord equipmentRecord = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.tag == Tags.Name.Player)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.actor = x.Actor;
                    this.actor.StatusController.SetEquipment(0, this.equipmentRecord);
                })
                .AddTo(this);
        }

        void Update()
        {
            if(this.actor == null)
            {
                return;
            }
            
            var velocity = new Vector2(Input.GetAxis("Horizontal"), 0.0f);
            if(velocity.sqrMagnitude > 0.0f)
            {
                this.actor.Broker.Publish(RequestMove.Get(velocity));
            }

            if(Input.GetButtonDown("Jump"))
            {
                this.actor.Broker.Publish(RequestJump.Get());
            }

            if(Input.GetButtonDown("Fire1"))
            {
                this.actor.Broker.Publish(RequestFire.Get());
            }
        }
    }
}
