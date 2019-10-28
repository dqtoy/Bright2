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

        public WeaponRecord EquipmentRecord { get; private set; }

        private readonly ReactiveProperty<float> coolTimeSeconds = new ReactiveProperty<float>();
        public IReactiveProperty<float> CoolTimeSeconds => this.coolTimeSeconds;

        public EquippedEquipment(GameObject owner)
        {
            this.owner = owner;
            this.owner.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    if(_this.EquipmentRecord != null)
                    {
                        if(_this.coolTimeSeconds.Value < _this.EquipmentRecord.CoolTimeSeconds)
                        {
                            _this.coolTimeSeconds.Value += Time.deltaTime;
                        }
                    }
                });
        }

        public void Change(WeaponRecord equipmentRecord)
        {
            this.EquipmentRecord = equipmentRecord;
            this.coolTimeSeconds.Value = equipmentRecord.CoolTimeSeconds;
        }

        public void ResetCoolTime()
        {
            this.coolTimeSeconds.Value = 0.0f;
        }

        /// <summary>
        /// クールタイムの割合を返す
        /// </summary>
        /// <remarks>
        /// 何も装備していない場合は常に<c>1</c>を返す
        /// </remarks>
        public float CoolTimeRate
        {
            get
            {
                if(this.EquipmentRecord == null)
                {
                    return 1.0f;
                }

                return Mathf.Clamp01(this.coolTimeSeconds.Value / this.EquipmentRecord.CoolTimeSeconds);
            }
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
                
                return this.coolTimeSeconds.Value >= this.EquipmentRecord.CoolTimeSeconds;
            }
        }
    }
}
