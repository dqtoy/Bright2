using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// <see cref="MasterData"/>が管理する要素のインターフェイス
    /// </summary>
    public interface IMasterDataList<E> where E : IMasterDataRecord
    {
        IReadOnlyList<E> Records { get; }
    }
}
