using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.WeaponControllers
{
    /// <summary>
    /// 装備中の武器を管理するクラス
    /// </summary>
    public sealed class EquippedWeapon
    {
        private GameObject owner;

        public InstanceWeapon InstanceWeapon { get; private set; }

        private readonly ReactiveProperty<float> coolTimeSeconds = new ReactiveProperty<float>();
        public IReactiveProperty<float> CoolTimeSeconds => this.coolTimeSeconds;

        public EquippedWeapon(GameObject owner)
        {
            this.owner = owner;
            this.owner.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    if(_this.InstanceWeapon != null)
                    {
                        if(_this.coolTimeSeconds.Value < _this.InstanceWeapon.WeaponRecord.CoolTimeSeconds)
                        {
                            _this.coolTimeSeconds.Value += Time.deltaTime;
                        }
                    }
                });
        }

        public void Change(InstanceWeapon instanceWeapon)
        {
            this.InstanceWeapon = instanceWeapon;
            this.coolTimeSeconds.Value = instanceWeapon.WeaponRecord.CoolTimeSeconds;
        }

        public void ResetCoolTime()
        {
            this.coolTimeSeconds.Value = 0.0f;
        }

        /// <summary>
        /// 装備中であるか返す
        /// </summary>
        public bool IsEquipped => this.InstanceWeapon != null;

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
                if(this.InstanceWeapon == null)
                {
                    return 1.0f;
                }

                return Mathf.Clamp01(this.coolTimeSeconds.Value / this.InstanceWeapon.WeaponRecord.CoolTimeSeconds);
            }
        }

        public bool CanFire
        {
            get
            {
                // 装備していない場合は攻撃出来ない
                if(this.InstanceWeapon == null)
                {
                    return false;
                }
                
                return this.coolTimeSeconds.Value >= this.InstanceWeapon.WeaponRecord.CoolTimeSeconds;
            }
        }
    }
}
