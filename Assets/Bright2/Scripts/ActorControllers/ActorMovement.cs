using HK.Bright2.ActorControllers.Messages;
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
        private Vector2 shrinkHorizontalRaycastBoxSize = default;

        [SerializeField]
        private float snapGroundCheckThreshold = default;

        [SerializeField]
        private float snapGroundDistance = default;

        [SerializeField]
        private Vector2 snapGroundOffsetOrigin = default;

        [SerializeField]
        private Vector2 snapGroundOffsetSize = default;

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
            this.CheckHorizontal();
            this.CheckVertical();
            t.localPosition += new Vector3(this.velocity.x, this.velocity.y, 0.0f);

#if UNITY_EDITOR
            this.lastVelocity = this.velocity;
#endif

            this.velocity = Vector2.zero;
        }

#if UNITY_EDITOR

        private Vector2 lastVelocity;

        private Vector2 lastHitHorizontalPoint;

        private Vector2 lastHitVerticalPoint;

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

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(this.lastHitHorizontalPoint.x, this.lastHitHorizontalPoint.y, 0.0f), 0.1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(this.lastHitVerticalPoint.x, this.lastHitVerticalPoint.y, 0.0f), 0.1f);
        }
#endif

        public void AddMove(Vector2 velocity)
        {
            this.velocity += velocity;
        }

        public void SetGravity(float gravity)
        {
            this.currentGravity = gravity;
        }

        private void AddGravity()
        {
            this.currentGravity += this.gravity * Time.deltaTime;
            this.velocity += Vector2.down * this.currentGravity * Time.deltaTime;
        }

        private void CheckVertical()
        {
            var t = this.actor.CachedTransform;
            var pos = t.localPosition;
            var origin = new Vector2(pos.x, pos.y) + this.boxCollider2D.offset;
            var size = this.boxCollider2D.size;
            size.x -= this.shrinkHorizontalRaycastBoxSize.x;
            var angle = t.localRotation.eulerAngles.z;
            var direction = Vector2.down;
            var distance = Mathf.Abs(this.velocity.y);
            var snapDistance = this.snapGroundDistance;

            // 落下中はスナップするか確認する
            if(this.velocity.y < this.snapGroundCheckThreshold)
            {
                var snapHit = Physics2D.BoxCast(origin + this.snapGroundOffsetOrigin, size + this.snapGroundOffsetSize, angle, direction, snapDistance, raycastIncludeLayerMask.value);
                if(snapHit.transform != null)
                {
                    var point = snapHit.point;
#if UNITY_EDITOR
                    this.lastHitVerticalPoint = point;
#endif
                    var v = this.velocity;
                    this.currentGravity = 0.0f;
                    t.localPosition = new Vector3(pos.x, point.y, pos.z);
                    v.y = 0.0f;
                    this.velocity = v;
                }
            }
            var hit = Physics2D.BoxCast(origin, size, angle, direction, distance, raycastIncludeLayerMask.value);
            if(hit.transform != null)
            {
                var point = hit.point;
                var diff = point.y - origin.y;

                // 移動方向と接地点差分の方向が一致していたら接地処理をする
                if((diff > 0 && this.velocity.y > 0) || (diff < 0 && this.velocity.y < 0))
                {
#if UNITY_EDITOR
                    this.lastHitVerticalPoint = point;
#endif
                    var v = this.velocity;
                    this.currentGravity = 0.0f;
                    var newY = v.y < 0.0f ? point.y : point.y - size.y;
                    t.localPosition = new Vector3(pos.x, newY, pos.z);
                    v.y = 0.0f;
                    this.velocity = v;
                }
            }
        }

        private void CheckHorizontal()
        {
            if(this.velocity.x == 0.0f)
            {
                this.actor.Broker.Publish(Idle.Get());
                return;
            }

            var v = this.velocity;
            var tempVelocity = v;
            var t = this.actor.CachedTransform;
            var pos = t.localPosition;
            var origin = new Vector2(pos.x, pos.y) + this.boxCollider2D.offset;
            var size = this.boxCollider2D.size;
            size.y -= this.shrinkHorizontalRaycastBoxSize.y;
            var angle = t.localRotation.eulerAngles.z;
            var direction = this.velocity.x > 0.0f ? Vector2.right : Vector2.left;
            var distance = Mathf.Abs(this.velocity.x);
            var hit = Physics2D.BoxCast(origin, size, angle, direction, distance, raycastIncludeLayerMask.value);
            if(hit.transform != null)
            {
                var point = hit.point;
                var diff = point.x - pos.x;

                if((diff > 0.0f && v.x > 0.0f) || (diff < 0.0f && v.x < 0.0f))
                {
#if UNITY_EDITOR
                    this.lastHitHorizontalPoint = point;
#endif

                    var offset = v.x > 0.0f ? -size.x * 0.5f : size.x * 0.5f;
                    t.localPosition = new Vector3(point.x + offset, pos.y, pos.z);

                    v.x = 0.0f;
                    this.velocity = v;
                }
            }

            this.actor.Broker.Publish(Move.Get(tempVelocity));
        }
    }
}
