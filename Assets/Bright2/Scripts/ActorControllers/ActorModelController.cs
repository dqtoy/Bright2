using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
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
        [SerializeField]
        private Transform modelParent = default;

        [SerializeField]
        private float leftDirection = default;

        [SerializeField]
        private float rightDirection = default;

        private Actor owner;

        private Dictionary<int, Renderer> models = null;

        private int currentModelId = 0;

        private Constants.Direction direction = Constants.Direction.None;

        void Awake()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);
        }

        public void Change(int nextModelId)
        {
            this.SetupModels();

            if(this.currentModelId != 0)
            {
                this.models[this.currentModelId].enabled = false;
            }

            this.currentModelId = nextModelId;
            this.models[this.currentModelId].enabled = true;
        }

        /// <summary>
        /// モデルを<paramref name="direction"/>方向へ向かせる
        /// </summary>
        public void Turn(Constants.Direction direction)
        {
            Assert.AreNotEqual(direction, Constants.Direction.None);

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

        private float GetDirectionRotation(Constants.Direction direction)
        {
            Assert.AreNotEqual(direction, Constants.Direction.None);

            return direction == Constants.Direction.Left ? this.leftDirection : this.rightDirection;
        }

        private float GetDirectionScale(Constants.Direction direction)
        {
            Assert.AreNotEqual(direction, Constants.Direction.None);

            return direction == Constants.Direction.Left ? -1.0f : 1.0f;
        }

        private void SetupModels()
        {
            if(this.models != null)
            {
                return;
            }

            this.models = new Dictionary<int, Renderer>();
            for (var i = 0; i < this.modelParent.childCount; i++)
            {
                var child = this.modelParent.GetChild(i);
                var model = child.GetComponentInChildren<Renderer>();
                Assert.IsNotNull(model);
                this.models.Add(Animator.StringToHash(child.name), model);
                model.enabled = false;
            }
        }
    }
}
