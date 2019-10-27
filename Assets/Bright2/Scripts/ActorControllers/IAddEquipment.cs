using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に装備品を追加するインターフェイス
    /// </summary>
    public interface IAddEquipment
    {
        /// <summary>
        /// 初期化
        /// </summary>
        void Setup(EquipmentRecord equipment);
    }
}
