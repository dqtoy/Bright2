using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using UnityEngine.Assertions;

namespace HK.Bright2.ItemControllers
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

        /// <summary>
        /// 大事なアイテムリスト
        /// </summary>
        private List<ImportantItemRecord> importantItems;
        public List<ImportantItemRecord> ImportantItems => this.importantItems = this.importantItems ?? new List<ImportantItemRecord>();

        private readonly Actor owner;

        public Inventory(Actor owner, ActorContext context)
        {
            this.owner = owner;
            this.Money = context.BasicStatus.Money;
        }

        public void AddMoney(int value)
        {
            this.Money += value;
            Assert.IsTrue(this.Money >= 0);
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

        public void AddImportantItem(ImportantItemRecord importantItemRecord)
        {
            this.importantItems = this.importantItems ?? new List<ImportantItemRecord>();
            this.importantItems.Add(importantItemRecord);
        }

        public bool IsEnough(int money)
        {
            return this.Money >= money;
        }

        public bool IsEnough(NeedItems needItems)
        {
            foreach(var n in needItems.Elements)
            {
                if(!this.IsEnough(n.MasterDataRecord, n.Amount))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsEnough(MasterDataRecord masterDataRecord, int amount)
        {
            return this.GetPossessionCount(masterDataRecord) >= amount;
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

        public void Consume(NeedItems needItems, List<InstanceWeapon> instanceWeapons, int money)
        {
            foreach(var element in needItems.Elements)
            {
                var record = element.MasterDataRecord;
                if(record is AccessoryRecord)
                {
                    var possessionCount = this.GetPossessionCount(record);
                    Assert.AreNotEqual(possessionCount, 0, "アクセサリーを所持していないのに消費しようとしました");
                    var equippedAccessoryIndex = -1;
                    var equippedAccessoryRecordIndex = -1;
                    var equippedAccessories = this.owner.StatusController.EquippedAccessories;
                    for (var i = 0; i < equippedAccessories.Count; i++)
                    {
                        if(equippedAccessories[i] < 0)
                        {
                            continue;
                        }
                        if (this.accessories[equippedAccessories[i]] == record)
                        {
                            equippedAccessoryIndex = i;
                            equippedAccessoryRecordIndex = equippedAccessories[i];
                            break;
                        }
                    }

                    if(possessionCount == 1)
                    {
                        // 装備中のアクセサリーだった場合は外す
                        if(equippedAccessoryIndex != -1)
                        {
                            this.owner.StatusController.ChangeEquippedAccessory(equippedAccessoryIndex, -1);
                        }

                        this.accessories.Remove(record as AccessoryRecord);
                        if(equippedAccessoryRecordIndex != -1)
                        {
                            this.owner.StatusController.DecreaseEquippedAccessoryIndex(equippedAccessoryRecordIndex);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < this.accessories.Count; i++)
                        {
                            // 装備していないアクセサリーを探して消費する
                            if(this.accessories[i] == record && i != equippedAccessoryRecordIndex)
                            {
                                this.accessories.RemoveAt(i);
                                this.owner.StatusController.DecreaseEquippedAccessoryIndex(i);
                                break;
                            }
                        }
                    }
                }
                else if(record is MaterialRecord)
                {
                    this.materials[(record as MaterialRecord)].Add(-element.Amount);
                }
                else if(record is WeaponRecord)
                {
                    // 何もしないけど次のAssertで引っかからないようにあえて書いておく
                }
                else
                {
                    Assert.IsTrue(false, $"{record.GetType()}は未定義の動作です");
                }
            }

            if(instanceWeapons != null)
            {
                foreach (var instanceWeapon in instanceWeapons)
                {
                    this.weapons.Remove(instanceWeapon);

                    // 装備中の武器だった場合は何も装備しなくする
                    var equippedWeaponIndex = this.owner.StatusController.EquippedWeapons.FindIndex(m => m.InstanceWeapon == instanceWeapon);
                    if (equippedWeaponIndex != -1)
                    {
                        this.owner.StatusController.RemoveEquippedWeapon(equippedWeaponIndex);
                    }
                }
            }

            this.AddMoney(-money);
        }

        private int GetPossessionCount(MasterDataRecord masterDataRecord)
        {
            var result = 0;

            if (masterDataRecord is WeaponRecord)
            {
                foreach (var w in this.weapons)
                {
                    result += (w.WeaponRecord == masterDataRecord) ? 1 : 0;
                }

                return result;
            }
            if (masterDataRecord is AccessoryRecord)
            {
                foreach (var a in this.accessories)
                {
                    result += (a == masterDataRecord) ? 1 : 0;
                }

                return result;
            }
            if (masterDataRecord is MaterialRecord)
            {
                var materialRecord = masterDataRecord as MaterialRecord;
                return this.Materials[materialRecord].Amount;
            }

            Assert.IsTrue(false, "未定義の動作です");
            return 0;
        }
    }
}
