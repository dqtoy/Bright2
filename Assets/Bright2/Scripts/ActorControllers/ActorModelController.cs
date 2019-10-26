using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>のモデルを制御するクラス
    /// </summary>
    public sealed class ActorModelController : MonoBehaviour
    {
        public enum Direction
        {
            None,
            Left,
            Right
        }

        [SerializeField]
        private Transform modelParent = default;

        [SerializeField]
        private float leftDirection = default;

        [SerializeField]
        private float rightDirection = default;

        private Actor owner;

        private readonly Dictionary<int, Renderer> dictionary = new Dictionary<int, Renderer>();

        private int currentModelId = 0;

        private Direction direction = Direction.None;

        void Awake()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);

            for (var i = 0; i < this.modelParent.childCount; i++)
            {
                var child = this.modelParent.GetChild(i);
                var model = child.GetComponentInChildren<Renderer>();
                Assert.IsNotNull(model);
                this.dictionary.Add(Animator.StringToHash(child.name), model);
                model.enabled = false;
            }

            this.currentModelId = ActorModelNames.Idle0;
            this.Change(this.currentModelId);

            this.owner.Broker.Receive<Move>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var direction = x.Velocity.x > 0.0f ? Direction.Right : Direction.Left;
                    _this.Turn(direction);
                })
                .AddTo(this);
        }

        public void Change(int nextModelId)
        {
            this.dictionary[this.currentModelId].enabled = false;
            this.currentModelId = nextModelId;
            this.dictionary[this.currentModelId].enabled = true;
        }

        /// <summary>
        /// モデルを<paramref name="direction"/>方向へ向かせる
        /// </summary>
        private void Turn(Direction direction)
        {
            Assert.AreNotEqual(direction, Direction.None);

            if(this.direction == direction)
            {
                return;
            }

            this.direction = direction;

            var rotation = this.modelParent.localRotation.eulerAngles;
            rotation.y = this.GetDirectionRotation(direction);
            this.modelParent.localRotation = Quaternion.Euler(rotation);

            var scale = this.modelParent.localScale;
            scale.x = this.GetDirectionScale(direction);
            this.modelParent.localScale = scale;
        }

        private float GetDirectionRotation(Direction direction)
        {
            Assert.AreNotEqual(direction, Direction.None);

            return direction == Direction.Left ? this.leftDirection : this.rightDirection;
        }

        private float GetDirectionScale(Direction direction)
        {
            Assert.AreNotEqual(direction, Direction.None);

            return direction == Direction.Left ? -1.0f : 1.0f;
        }
    }
}
