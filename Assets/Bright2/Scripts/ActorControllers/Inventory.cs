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
        public List<InstanceWeapon> Weapons { get; private set; }

        /// <summary>
        /// アクセサリーリスト
        /// </summary>
        public List<AccessoryRecord> Accessories { get; private set; }

        /// <summary>
        /// 素材リスト
        /// </summary>
        public Dictionary<MaterialRecord, InstanceMaterial> Materials { get; private set; }

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
            this.Weapons = this.Weapons ?? new List<InstanceWeapon>();
            var instanceWeapon = new InstanceWeapon(weaponRecord);
            this.Weapons.Add(instanceWeapon);

            return instanceWeapon;
        }

        public void AddAccessory(AccessoryRecord accessoryRecord)
        {
            this.Accessories = this.Accessories ?? new List<AccessoryRecord>();
            this.Accessories.Add(accessoryRecord);
        }

        public void AddMaterial(MaterialRecord materialRecord, int amount)
        {
            this.Materials = this.Materials ?? new Dictionary<MaterialRecord, InstanceMaterial>();
            var instance = default(InstanceMaterial);
            if (!this.Materials.TryGetValue(materialRecord, out instance))
            {
                instance = new InstanceMaterial(materialRecord);
                this.Materials.Add(materialRecord, instance);
            }

            instance.Add(amount);
        }
    }
}
