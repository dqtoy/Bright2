using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// <see cref="Id"/>を持つマスターデータレコードのインターフェイス
    /// </summary>
    public interface IMasterDataRecordId : IMasterDataRecord
    {
        string Id { get; }
    }
}
