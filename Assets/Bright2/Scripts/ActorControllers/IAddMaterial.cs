using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に素材を追加するインターフェイス
    /// </summary>
    public interface IAddMaterial
    {
        /// <summary>
        /// 初期化
        /// </summary>
        void Setup(MaterialRecord material);
    }
}
