namespace HK.Bright2.Database
{
    /// <summary>
    /// <see cref="IMasterDataRecordRecipe"/>を管理する<see cref="MasterDataList{E}"/>
    /// </summary>
    public interface IMasterDataListRecipe<E> : IMasterDataList<E> where E : IMasterDataRecord, IMasterDataRecordRecipe
    {
    }
}
