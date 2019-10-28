using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 装備中の武器のアイコンUIを制御するクラス
    /// </summary>
    public sealed class EquippedWeaponIconController : MonoBehaviour
    {
        /// <summary>
        /// 監視する武器のインデックス
        /// </summary>
        [SerializeField]
        private int observeEquippedWeaponIndex = default;

        [SerializeField]
        private Slider coolTimeSlider = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.tag == Tags.Name.Player)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var equippedWeapon = x.Actor.StatusController.EquippedWeapons[_this.observeEquippedWeaponIndex];
                    _this.ObserveCoolTime(equippedWeapon);
                })
                .AddTo(this);
        }

        private void ObserveCoolTime(EquippedWeapon equippedWeapon)
        {
            equippedWeapon.CoolTimeSeconds
                .SubscribeWithState2(this, equippedWeapon, (_, _this, e) =>
                {
                    _this.UpdateCoolTimeSlider(e.CoolTimeRate);
                })
                .AddTo(this);
        }

        private void UpdateCoolTimeSlider(float value)
        {
            this.coolTimeSlider.value = 1.0f - value;
        }
    }
}
