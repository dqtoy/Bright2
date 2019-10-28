using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
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

        private int horizontalIndex = 0;

        private int verticalIndex = 0;

        private IReadOnlyList<InstanceWeapon> instanceWeapons;

        void Awake()
        {
            this.canvasGroup.alpha = 0.0f;

            Broker.Global.Receive<RequestShowWeaponGridUI>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.horizontalIndex = 0;
                    _this.verticalIndex = 0;
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

        private List<WeaponGridScrollViewItemData> CreateItems(IReadOnlyList<InstanceWeapon> instanceWeapons)
        {
            this.instanceWeapons = instanceWeapons;
            var result = new List<WeaponGridScrollViewItemData>();

            if(instanceWeapons == null)
            {
                return result;
            }

            var verticalIndex = 0;
            var itemData = new WeaponGridScrollViewItemData(verticalIndex);
            for (var i = 0; i < instanceWeapons.Count; i++)
            {
                if (!itemData.CanAddRecord)
                {
                    result.Add(itemData);
                    verticalIndex++;
                    itemData = new WeaponGridScrollViewItemData(verticalIndex);
                }

                itemData.Records.Add(instanceWeapons[i].WeaponRecord);
            }

            result.Add(itemData);

            return result;
        }

        void IControllableUserInput.UpdateInput()
        {
            if (Input.GetButtonDown(InputName.Left))
            {
                this.horizontalIndex--;
                this.ClampHorizontalIndex();
                this.scrollView.UpdateSelectIndex(this.SelectIndex);
            }
            if (Input.GetButtonDown(InputName.Right))
            {
                this.horizontalIndex++;
                this.ClampHorizontalIndex();
                this.scrollView.UpdateSelectIndex(this.SelectIndex);
            }
            if (Input.GetButtonDown(InputName.Up))
            {
                this.verticalIndex--;
                this.ClampVerticalIndex();
                this.scrollView.UpdateSelectIndex(this.SelectIndex);
            }
            if (Input.GetButtonDown(InputName.Down))
            {
                this.verticalIndex++;
                this.ClampVerticalIndex();
                this.scrollView.UpdateSelectIndex(this.SelectIndex);
            }
            if(Input.GetButtonDown(InputName.Decide))
            {
                Broker.Global.Publish(SelectInstanceWeaponIndex.Get(this.SelectIndex));
            }
            if(Input.GetButtonDown(InputName.Cancel))
            {
                Broker.Global.Publish(HideWeaponGridUI.Get(this));
            }
        }

        private void ClampHorizontalIndex()
        {
            if(this.horizontalIndex < 0)
            {
                this.horizontalIndex = WeaponGridScrollViewCell.ElementMax - 1;
                this.DecreateHorizontalIndex();
            }
            else if(
                this.horizontalIndex >= WeaponGridScrollViewCell.ElementMax ||
                this.horizontalIndex >= this.instanceWeapons.Count ||
                this.SelectIndex >= this.instanceWeapons.Count
                )
            {
                this.horizontalIndex = 0;
            }
        }

        /// <summary>
        /// <see cref="horizontalIndex"/>が配列外に存在する場合は減算を行う
        /// </summary>
        private void DecreateHorizontalIndex()
        {
            var max = this.instanceWeapons.Count - 1;
            while (max < this.SelectIndex)
            {
                this.horizontalIndex--;
            }
        }

        private void ClampVerticalIndex()
        {
            var verticalMax = this.instanceWeapons.Count / WeaponGridScrollViewCell.ElementMax;
            if(this.verticalIndex < 0)
            {
                this.verticalIndex = 0;
            }
            else if(this.verticalIndex > verticalMax)
            {
                this.verticalIndex = verticalMax;
            }

            this.DecreateHorizontalIndex();
        }

        private int SelectIndex => (this.verticalIndex * WeaponGridScrollViewCell.ElementMax) + this.horizontalIndex;
    }
}
