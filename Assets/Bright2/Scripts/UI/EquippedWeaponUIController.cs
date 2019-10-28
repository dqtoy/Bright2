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

        void Awake()
        {
            Broker.Global.Receive<RequestControlUserInputEquippedWeaponUI>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    Broker.Global.Publish(BeginControlUserInputEquippedWeaponUI.Get(_this));
                })
                .AddTo(this);
        }

        void IControllableUserInput.UpdateInput()
        {
            if (Input.GetButtonDown(InputName.Left))
            {
                this.selectIndex = 0;
            }
            if (Input.GetButtonDown(InputName.Up))
            {
                this.selectIndex = 1;
            }
            if (Input.GetButtonDown(InputName.Right))
            {
                this.selectIndex = 2;
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
