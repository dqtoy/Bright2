using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// <see cref="Constants.Direction"/>から同期を取るインターフェイス
    /// </summary>
    public interface ISyncDirection
    {
        /// <summary>
        /// 同期を取る
        /// </summary>
        void Sync(Constants.Direction direction);
    }
}
