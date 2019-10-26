using HK.Bright2.Database;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// 装備中の装備品を管理するクラス
    /// </summary>
    public sealed class EquippedEquipment
    {
        private GameObject owner;

        public EquipmentRecord EquipmentRecord { get; private set; }

        private float coolTimeSeconds;

        public EquippedEquipment(GameObject owner)
        {
            this.owner = owner;
            this.owner.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.coolTimeSeconds = Time.deltaTime;
                });
        }

        public void Change(EquipmentRecord equipmentRecord)
        {
            this.EquipmentRecord = equipmentRecord;
            this.coolTimeSeconds = equipmentRecord.CoolTimeSeconds;
        }

        public bool CanFire
        {
            get
            {
                // 装備していない場合は攻撃出来ない
                if(this.EquipmentRecord == null)
                {
                    return false;
                }
                
                return this.coolTimeSeconds >= this.EquipmentRecord.CoolTimeSeconds;
            }
        }
    }
}
