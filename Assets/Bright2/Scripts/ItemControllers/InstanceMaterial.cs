using HK.Bright2.Database;

namespace HK.Bright2.ItemControllers
{
    /// <summary>
    /// 素材のインスタンスデータ
    /// </summary>
    public sealed class InstanceMaterial
    {
        public readonly MaterialRecord MaterialRecord;
        
        public int Amount { get; private set; }

        public InstanceMaterial(MaterialRecord materialRecord)
        {
            this.MaterialRecord = materialRecord;
        }

        public void Add(int amount)
        {
            this.Amount += amount;
        }
    }
}
