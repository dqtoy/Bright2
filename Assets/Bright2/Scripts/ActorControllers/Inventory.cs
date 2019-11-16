using System.Collections.Generic;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Database;
using HK.Bright2.MaterialControllers;
using HK.Bright2.WeaponControllers;
using UnityEngine;
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
    }
}
