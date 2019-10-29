using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// 指定した回転値を適用するだけのギミックデコレーター
    /// </summary>
    public sealed class ConstantRotation : MonoBehaviour, IGimmickDecorator
    {
        [SerializeField]
        private Transform target = default;

        [SerializeField]
        private Vector3 rotation = default;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            this.target.rotation = Quaternion.Euler(this.rotation);
        }
    }
}
