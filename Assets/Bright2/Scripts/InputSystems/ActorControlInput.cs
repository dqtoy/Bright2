using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;

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

        void Start()
        {
            this.actor.StatusController.SetEquipment(this.equipmentRecord);
        }

        void Update()
        {
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
