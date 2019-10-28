using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.InputSystems;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 武器グリッドUIを制御するクラス
    /// </summary>
    public sealed class WeaponGridUIController : MonoBehaviour, IControllableUserInput
    {
        [SerializeField]
        private CanvasGroup canvasGroup = default;

        [SerializeField]
        private WeaponGridScrollView scrollView = default;

        void Awake()
        {
            this.canvasGroup.alpha = 0.0f;

            Broker.Global.Receive<RequestShowWeaponGridUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.canvasGroup.alpha = 1.0f;
                    _this.scrollView.UpdateData(_this.CreateItems(x.Records));
                    Broker.Global.Publish(ShowWeaponGridUI.Get(_this));
                })
                .AddTo(this);
            Broker.Global.Receive<HideWeaponGridUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.canvasGroup.alpha = 0.0f;
                })
                .AddTo(this);
        }

        private List<WeaponGridScrollViewItemData> CreateItems(IReadOnlyList<WeaponRecord> weaponRecords)
        {
            var result = new List<WeaponGridScrollViewItemData>();

            if(weaponRecords == null)
            {
                return result;
            }

            var itemData = new WeaponGridScrollViewItemData();
            for (var i = 0; i < weaponRecords.Count; i++)
            {
                if (!itemData.CanAddRecord)
                {
                    result.Add(itemData);
                    itemData = new WeaponGridScrollViewItemData();
                }

                itemData.Records.Add(weaponRecords[i]);
            }

            result.Add(itemData);

            return result;
        }

        void IControllableUserInput.UpdateInput()
        {
            if (Input.GetButtonDown(InputName.Left))
            {
                Debug.Log("Left");
            }
            if (Input.GetButtonDown(InputName.Right))
            {
                Debug.Log("Right");
            }
            if (Input.GetButtonDown(InputName.Up))
            {
                Debug.Log("Up");
            }
            if (Input.GetButtonDown(InputName.Down))
            {
                Debug.Log("Down");
            }
            if(Input.GetButtonDown(InputName.Cancel))
            {
                Broker.Global.Publish(HideWeaponGridUI.Get(this));
            }
        }
    }
}
