using HK.Bright2.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に大事なアイテムを追加するインターフェイス
    /// </summary>
    public interface IAddImportantItem
    {
        /// <summary>
        /// 初期化
        /// </summary>
        void Setup(ImportantItemRecord importantItem);
    }
}
