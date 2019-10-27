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
    /// 装備中の装備品のアイコンUIを制御するクラス
    /// </summary>
    public sealed class EquippedEquipmentIconController : MonoBehaviour
    {
        /// <summary>
        /// 監視する装備品のインデックス
        /// </summary>
        [SerializeField]
        private int observeEquippedEquipmentIndex = default;

        [SerializeField]
        private Slider coolTimeSlider = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .Where(x => x.Actor.tag == Tags.Name.Player)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var equippedEquipment = x.Actor.StatusController.EquippedEquipments[_this.observeEquippedEquipmentIndex];
                    _this.ObserveCoolTime(equippedEquipment);
                })
                .AddTo(this);
        }

        private void ObserveCoolTime(EquippedEquipment equippedEquipment)
        {
            equippedEquipment.CoolTimeSeconds
                .SubscribeWithState2(this, equippedEquipment, (_, _this, e) =>
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
