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
    /// <see cref="Actor"/>をユーザーの入力によって制御するクラス
    /// </summary>
    public sealed class ActorControlOnUserInput : IControllableUserInput
    {
        private readonly Actor actor;

        public ActorControlOnUserInput(Actor actor)
        {
            this.actor = actor;
        }

        void IControllableUserInput.UpdateInput()
        {
            if (this.actor == null)
            {
                return;
            }

            var velocity = new Vector2(Input.GetAxis("Horizontal"), 0.0f);
            if (velocity.sqrMagnitude > 0.0f)
            {
                this.actor.Broker.Publish(RequestMove.Get(velocity));
            }

            if (Input.GetButtonDown("InvokeGameEvent"))
            {
                this.actor.Broker.Publish(RequestInvokeGameEvent.Get());
            }

            if (Input.GetButtonDown("Jump"))
            {
                this.actor.Broker.Publish(RequestJump.Get());
            }

            this.PublishRequestFire(InputName.Fire1, 0);
            this.PublishRequestFire(InputName.Fire2, 1);
            this.PublishRequestFire(InputName.Fire3, 2);

            this.PublishRequestTerminationFire(InputName.Fire1, 0);
            this.PublishRequestTerminationFire(InputName.Fire2, 1);
            this.PublishRequestTerminationFire(InputName.Fire3, 2);
        }

        private void PublishRequestFire(string buttonName, int equippedWeaponIndex)
        {
            if (Input.GetButtonDown(buttonName))
            {
                this.actor.Broker.Publish(RequestFire.Get(equippedWeaponIndex));
            }
        }

        private void PublishRequestTerminationFire(string buttonName, int equippedWeaponIndex)
        {
            if (Input.GetButtonUp(buttonName))
            {
                this.actor.Broker.Publish(RequestTerminationFire.Get(equippedWeaponIndex));
            }
        }
    }
}
