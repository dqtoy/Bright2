using HK.Bright2.Database;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// 装備中の武器を管理するクラス
    /// </summary>
    public sealed class EquippedWeapon
    {
        private GameObject owner;

        public WeaponRecord WeaponRecord { get; private set; }

        private readonly ReactiveProperty<float> coolTimeSeconds = new ReactiveProperty<float>();
        public IReactiveProperty<float> CoolTimeSeconds => this.coolTimeSeconds;

        public EquippedWeapon(GameObject owner)
        {
            this.owner = owner;
            this.owner.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    if(_this.WeaponRecord != null)
                    {
                        if(_this.coolTimeSeconds.Value < _this.WeaponRecord.CoolTimeSeconds)
                        {
                            _this.coolTimeSeconds.Value += Time.deltaTime;
                        }
                    }
                });
        }

        public void Change(WeaponRecord weaponRecord)
        {
            this.WeaponRecord = weaponRecord;
            this.coolTimeSeconds.Value = weaponRecord.CoolTimeSeconds;
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
                if(this.WeaponRecord == null)
                {
                    return 1.0f;
                }

                return Mathf.Clamp01(this.coolTimeSeconds.Value / this.WeaponRecord.CoolTimeSeconds);
            }
        }

        public bool CanFire
        {
            get
            {
                // 装備していない場合は攻撃出来ない
                if(this.WeaponRecord == null)
                {
                    return false;
                }
                
                return this.coolTimeSeconds.Value >= this.WeaponRecord.CoolTimeSeconds;
            }
        }
    }
}
