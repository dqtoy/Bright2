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

            if (Input.GetButtonDown("Fire1"))
            {
                this.actor.Broker.Publish(RequestFire.Get(0));
            }

            if (Input.GetButtonDown("Fire2"))
            {
                this.actor.Broker.Publish(RequestFire.Get(1));
            }

            if (Input.GetButtonDown("Fire3"))
            {
                this.actor.Broker.Publish(RequestFire.Get(2));
            }
        }
    }
}
