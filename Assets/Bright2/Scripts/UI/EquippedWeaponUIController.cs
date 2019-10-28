using System.Collections.Generic;
using System.Linq;
using HK.Bright2.InputSystems;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 装備中の武器UIを制御するクラス
    /// </summary>
    public sealed class EquippedWeaponUIController : MonoBehaviour, IControllableUserInput
    {
        private int selectIndex;

        private List<EquippedWeaponIconController> iconControllers;

        void Awake()
        {
            Broker.Global.Receive<RequestControlUserInputEquippedWeaponUI>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.SetSelectIcon(0);
                    Broker.Global.Publish(BeginControlUserInputEquippedWeaponUI.Get(_this));
                })
                .AddTo(this);

            Broker.Global.Receive<EndControlUserInputEquippedWeaponUI>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.SetSelectIcon(-1);
                })
                .AddTo(this);

            this.iconControllers = this.GetComponentsInChildren<EquippedWeaponIconController>().ToList();
            Assert.AreEqual(this.iconControllers.Count, Constants.EquippedWeaponMax);
            this.iconControllers.Sort((x, y) => x.ObserveEquippedWeaponIndex - y.ObserveEquippedWeaponIndex);

            this.SetSelectIcon(-1);
        }

        private void SetSelectIcon(int newIndex)
        {
            if(this.selectIndex != -1)
            {
                this.iconControllers[this.selectIndex].SetIsFocus(false);
            }

            this.selectIndex = newIndex;

            if(this.selectIndex != -1)
            {
                this.iconControllers[this.selectIndex].SetIsFocus(true);
            }
        }

        void IControllableUserInput.UpdateInput()
        {
            if (Input.GetButtonDown(InputName.Left))
            {
                this.SetSelectIcon(0);
            }
            if (Input.GetButtonDown(InputName.Up))
            {
                this.SetSelectIcon(1);
            }
            if (Input.GetButtonDown(InputName.Right))
            {
                this.SetSelectIcon(2);
            }
            if(Input.GetButtonDown(InputName.Decide))
            {
                Broker.Global.Publish(SelectEquippedWeaponIndex.Get(this.selectIndex));
            }
            if(Input.GetButtonDown(InputName.Cancel))
            {
                Broker.Global.Publish(EndControlUserInputEquippedWeaponUI.Get(this));
            }
        }
    }
}
