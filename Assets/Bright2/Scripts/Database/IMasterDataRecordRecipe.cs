using HK.Bright2.ItemControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Database
{
    /// <summary>
    /// 何かしらのアイテムを生成するレシピを持つマスターデータ
    /// </summary>
    public interface IMasterDataRecordRecipe : IMasterDataRecord
    {
        /// <summary>
        /// 必要なアイテム
        /// </summary>
        NeedItems NeedItems { get; }
    }
}
