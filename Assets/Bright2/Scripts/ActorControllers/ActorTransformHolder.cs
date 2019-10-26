using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// <see cref="Actor"/>を構成するのに必要な<see cref="Transform"/>を保持するクラス
    /// </summary>
    public sealed class ActorTransformHolder : MonoBehaviour
    {
        [SerializeField]
        private Transform leftEquipmentOrigin = default;
        /// <summary>
        /// 左方向の装備品の原点
        /// </summary>
        public Transform LeftEquipmentOrigin => this.leftEquipmentOrigin;

        [SerializeField]
        private Transform rightEquipmentOrigin = default;
        /// <summary>
        /// 右方向の装備品の原点
        /// </summary>
        public Transform RightEquipmentOrigin => this.rightEquipmentOrigin;
    }
}
