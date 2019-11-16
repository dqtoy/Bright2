using System.Collections.Generic;
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
        public int Money { get; private set; }

        public List<InstanceWeapon> Weapons { get; private set; }

        public List<AccessoryRecord> Accessories { get; private set; }

        public Dictionary<MaterialRecord, InstanceMaterial> Materials { get; private set; }

        private readonly Actor owner;

        public Inventory(Actor owner)
        {
            this.owner = owner;
        }
    }
}
