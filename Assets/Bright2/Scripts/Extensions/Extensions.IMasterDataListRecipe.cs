using HK.Bright2.Database;
using System.Collections.Generic;
using HK.Bright2.ItemControllers;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="IMasterDataListRecipe{E}"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 表示可能なレシピのリストを返す
        /// </summary>
        public static IReadOnlyList<E> GetViewableRecipes<E>(this IMasterDataListRecipe<E> self, Inventory inventory)
            where E : IMasterDataRecordRecipe
        {
            var result = new List<E>();

            foreach (var r in self.Records)
            {
                if (!r.NeedItems.IsViewableList(inventory))
                {
                    continue;
                }

                result.Add(r);
            }

            return result;
        }
    }
}
