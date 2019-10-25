using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>の移動処理を制御するクラス
    /// </summary>
    public sealed class ActorMovement : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D boxCollider2D = default;

        private Actor actor;

        private Vector2 velocity;

        void Awake()
        {
            this.actor = this.GetComponent<Actor>();

            Assert.IsNotNull(this.actor);
            Assert.IsNotNull(this.boxCollider2D);
        }

        void Update()
        {
            var t = this.actor.CachedTransform;
            t.localPosition += new Vector3(this.velocity.x, this.velocity.y, 0.0f);
            this.velocity = Vector2.zero;
        }

        public void AddMove(Vector2 velocity)
        {
            this.velocity += velocity;
        }
    }
}
