using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に衝突したオブジェクトに対してアクションを行うクラス
    /// </summary>
    public sealed class ActorColliderActionController : MonoBehaviour
    {
        private Actor owner;

        void Awake()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            var reactionObjects = other.GetComponentsInChildren<IActorReactionOnTriggerEnter2D>();
            foreach(var r in reactionObjects)
            {
                r.Do(this.owner);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            var reactionObjects = other.GetComponentsInChildren<IActorReactionOnTriggerExit2D>();
            foreach (var r in reactionObjects)
            {
                r.Do(this.owner);
            }
        }
    }
}
