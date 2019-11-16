using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.ItemControllers;
using HK.Bright2.MaterialControllers;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 所持品を管理するクラス
    /// </summary>
    public sealed class Inventory
    {
        /// <summary>
        /// 所持金
        /// </summary>
        public int Money { get; private set; }

        /// <summary>
        /// 武器リスト
        /// </summary>
        private List<InstanceWeapon> weapons;
        public List<InstanceWeapon> Weapons => this.weapons = this.weapons ?? new List<InstanceWeapon>();

        /// <summary>
        /// アクセサリーリスト
        /// </summary>
        private List<AccessoryRecord> accessories;
        public List<AccessoryRecord> Accessories => this.accessories = this.accessories ?? new List<AccessoryRecord>();

        /// <summary>
        /// 素材リスト
        /// </summary>
        private Dictionary<MaterialRecord, InstanceMaterial> materials;
        public Dictionary<MaterialRecord, InstanceMaterial> Materials => this.materials = this.materials ?? new Dictionary<MaterialRecord, InstanceMaterial>();

        private readonly Actor owner;

        public Inventory(Actor owner, ActorContext context)
        {
            this.owner = owner;
            this.Money = context.BasicStatus.Money;
        }

        public void AddMoney(int value)
        {
            this.Money += value;
        }

        public InstanceWeapon AddWeapon(WeaponRecord weaponRecord)
        {
            this.weapons = this.weapons ?? new List<InstanceWeapon>();
            var instanceWeapon = new InstanceWeapon(weaponRecord);
            this.weapons.Add(instanceWeapon);

            return instanceWeapon;
        }

        public void AddAccessory(AccessoryRecord accessoryRecord)
        {
            this.accessories = this.accessories ?? new List<AccessoryRecord>();
            this.accessories.Add(accessoryRecord);
        }

        public void AddMaterial(MaterialRecord materialRecord, int amount)
        {
            this.materials = this.materials ?? new Dictionary<MaterialRecord, InstanceMaterial>();
            var instance = default(InstanceMaterial);
            if (!this.Materials.TryGetValue(materialRecord, out instance))
            {
                instance = new InstanceMaterial(materialRecord);
                this.materials.Add(materialRecord, instance);
            }

            instance.Add(amount);
        }

        public bool IsEnough(MasterDataRecord masterDataRecord, int amount)
        {
            var result = 0;

            if(masterDataRecord is WeaponRecord)
            {
                foreach(var w in this.weapons)
                {
                    result += (w.WeaponRecord == masterDataRecord) ? 1 : 0;
                }

                return result >= amount;
            }
            if(masterDataRecord is AccessoryRecord)
            {
                foreach(var a in this.accessories)
                {
                    result += (a == masterDataRecord) ? 1 : 0;
                }

                return result >= amount;
            }
            if(masterDataRecord is MaterialRecord)
            {
                var materialRecord = masterDataRecord as MaterialRecord;
                return this.Materials[materialRecord].Amount >= amount;
            }

            Assert.IsTrue(false, "未定義の動作です");
            return false;
        }

        public bool Contains(MasterDataRecord masterDataRecord)
        {
            if (masterDataRecord is WeaponRecord)
            {
                foreach (var w in this.weapons)
                {
                    if(w.WeaponRecord == masterDataRecord)
                    {
                        return true;
                    }
                }

                return false;
            }
            if (masterDataRecord is AccessoryRecord)
            {
                return this.accessories.Contains(masterDataRecord as AccessoryRecord);
            }
            if (masterDataRecord is MaterialRecord)
            {
                return this.materials.ContainsKey(masterDataRecord as MaterialRecord);
            }

            Assert.IsTrue(false, "未定義の動作です");
            return false;
        }
    }
}
