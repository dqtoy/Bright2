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
        private float forceMin = default;

        [SerializeField]
        private float forceMax = default;

        [SerializeField]
        private float offsetAngle = default;

        [SerializeField]
        private float randomAngle = default;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            var halfRandomAngle = randomAngle * 0.5f;
            var random = Random.Range(-halfRandomAngle, halfRandomAngle);
            var force = Random.Range(this.forceMin, this.forceMax);
            this.transform.rotation *= Quaternion.AngleAxis(this.offsetAngle, Vector3.forward);
            this.transform.rotation *= Quaternion.AngleAxis(random, Vector3.forward);
            this.controlledRigidbody2D.AddForce(this.transform.right * force);
        }
    }
}
