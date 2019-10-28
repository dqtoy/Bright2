using System.Collections.Generic;
using HK.Bright2.Database;
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
    public sealed class WeaponGridUIController : MonoBehaviour
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
                })
                .AddTo(this);
        }

        private List<WeaponGridScrollViewItemData> CreateItems(IReadOnlyList<WeaponRecord> weaponRecords)
        {
            var result = new List<WeaponGridScrollViewItemData>();
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
    }
}
