using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AddForce : MonoBehaviour, IGimmickDecorator
    {
        [SerializeField]
        private Rigidbody2D controlledRigidbody2D = default;

        [SerializeField]
        private float force = default;

        [SerializeField]
        private float randomAngle = default;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            var random = Random.Range(0.0f, this.randomAngle);
            this.transform.rotation *= Quaternion.AngleAxis(random, this.transform.forward);
            this.controlledRigidbody2D.AddForce(this.transform.right * this.force);
        }
    }
}
