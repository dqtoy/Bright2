using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;

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
        private float speed = default;

        [SerializeField]
        private float jumpPower = default;

        void Update()
        {
            var velocity = new Vector2(Input.GetAxis("Horizontal") * this.speed * Time.deltaTime, 0.0f);
            this.actor.Movement.AddMove(velocity);

            if(Input.GetButtonDown("Jump"))
            {
                this.actor.Movement.SetGravity(-this.jumpPower);
            }
        }
    }
}
