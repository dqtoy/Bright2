using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
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
        public int ObserveEquippedWeaponIndex => this.observeEquippedWeaponIndex;

        [SerializeField]
        private Image icon = default;

        [SerializeField]
        private Slider coolTimeSlider = default;

        [SerializeField]
        private CanvasGroup focusEffectCanvasGroup = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.CompareTag(Tags.Name.Player))
                .SubscribeWithState(this, (x, _this) =>
                {
                    var actor = x.Actor;
                    _this.ApplyIcon(actor);
                    _this.ObserveActor(actor);
                })
                .AddTo(this);

            this.SetIsFocus(false);
        }

        public void SetIsFocus(bool isFocus)
        {
            this.focusEffectCanvasGroup.alpha = isFocus ? 1.0f : 0.0f;
        }

        private void ObserveActor(Actor actor)
        {
            this.ObserveChangedEquippedWeapon(actor);

            var equippedWeapon = actor.StatusController.EquippedWeapons[this.observeEquippedWeaponIndex];
            this.ObserveCoolTime(equippedWeapon);
        }

        private void ObserveChangedEquippedWeapon(Actor actor)
        {
            actor.Broker.Receive<ChangedEquippedWeapon>()
                .Where(x => x.Index == this.observeEquippedWeaponIndex)
                .SubscribeWithState2(this, actor, (x, _this, a) =>
                {
                    _this.ApplyIcon(a);
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

        private void ApplyIcon(Actor actor)
        {
            var equippedWeapon = actor.StatusController.EquippedWeapons[this.observeEquippedWeaponIndex];
            if(!equippedWeapon.IsEquipped)
            {
                this.icon.sprite = null;
                return;
            }

            var weaponRecord = equippedWeapon.InstanceWeapon.WeaponRecord;
            this.icon.sprite = weaponRecord.Icon;
        }

        private void UpdateCoolTimeSlider(float value)
        {
            this.coolTimeSlider.value = 1.0f - value;
        }
    }
}
