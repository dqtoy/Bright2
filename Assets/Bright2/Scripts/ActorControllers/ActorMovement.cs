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

        [SerializeField]
        private LayerMask raycastIncludeLayerMask = default;

        [SerializeField]
        private float gravity = default;

        private Actor actor;

        private Vector2 velocity;

        private float currentGravity = 0.0f;

        void Awake()
        {
            this.actor = this.GetComponent<Actor>();

            Assert.IsNotNull(this.actor);
            Assert.IsNotNull(this.boxCollider2D);
        }

        void Update()
        {
            this.AddGravity();

            var t = this.actor.CachedTransform;
            this.CheckGround();
            t.localPosition += new Vector3(this.velocity.x, this.velocity.y, 0.0f);

#if UNITY_EDITOR
            this.lastVelocity = this.velocity;
#endif

            this.velocity = Vector2.zero;
        }

#if UNITY_EDITOR

        private Vector2 lastVelocity;

        void OnDrawGizmosSelected()
        {
            if(this.actor == null)
            {
                return;
            }

            var from = this.actor.CachedTransform.position;
            var to = from + new Vector3(this.lastVelocity.x, this.lastVelocity.y, 0.0f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(from, to);
        }
#endif

        public void AddMove(Vector2 velocity)
        {
            this.velocity += velocity;
        }

        private void AddGravity()
        {
            this.currentGravity += this.gravity * Time.deltaTime;
            this.velocity += Vector2.down * this.currentGravity * Time.deltaTime;
        }

        private void CheckGround()
        {
            var t = this.actor.CachedTransform;
            var pos = t.localPosition;
            var origin = new Vector2(pos.x, pos.y) + this.boxCollider2D.offset;
            var size = this.boxCollider2D.size;
            var angle = t.localRotation.eulerAngles.z;
            var direction = Vector2.down;
            var distance = Mathf.Abs(this.velocity.y);
            var raycastHit2D = Physics2D.BoxCast(origin, size, angle, direction, distance, raycastIncludeLayerMask.value);
            if(raycastHit2D.transform != null)
            {
                var v = this.velocity;
                v.y = 0.0f;
                this.velocity = v;
                this.currentGravity = 0.0f;
                Debug.Log($"Hit {raycastHit2D.transform.name}");
                t.localPosition = new Vector3(pos.x, raycastHit2D.point.y, pos.z);
            }
        }
    }
}
