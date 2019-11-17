using System;
using System.Collections.Generic;
using System.Linq;
using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.ItemControllers;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="ISelectConsumeInstanceWeapons"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        public static void StartSelectConsumeInstanceWeapon(
            this ISelectConsumeInstanceWeapons self,
            Actor actor,
            NeedItems needItems,
            Action<List<InstanceWeapon>> OnCompleteSelect,
            Action OnCancel
            )
        {
            var targetWeaponRecords = needItems.GetNeedWeaponRecords();
            var selectConsumeInstanceWeapons = new List<InstanceWeapon>();
            var targetWeaponRecord = GetTargetWeaponRecord(targetWeaponRecords, selectConsumeInstanceWeapons);
            self.InternalStartSelectConsumeInstanceWeapon(
                actor,
                targetWeaponRecord,
                targetWeaponRecords,
                selectConsumeInstanceWeapons,
                OnCompleteSelect,
                OnCancel
            );
        }

        private static void InternalStartSelectConsumeInstanceWeapon(
            this ISelectConsumeInstanceWeapons self,
            Actor actor,
            WeaponRecord targetWeaponRecord,
            List<WeaponRecord> targetWeaponRecords,
            List<InstanceWeapon> selectConsumeInstanceWeapons,
            Action<List<InstanceWeapon>> OnCompleteSelect,
            Action OnCancel
        )
        {
            Assert.IsNotNull(targetWeaponRecord, $"対象となる{typeof(WeaponRecord)}がありません");

            // 所持している武器から対象となる武器を抽出
            var targetInstanceWeapons = actor.StatusController.Inventory.Weapons
                .Where(x => x.WeaponRecord == targetWeaponRecord)
                .ToList();

            Broker.Global.Publish(RequestShowGridUI.Get(targetInstanceWeapons, i =>
            {
                selectConsumeInstanceWeapons.Add(targetInstanceWeapons[i]);

                Broker.Global.Publish(RequestHideGridUI.Get());

                var nextTargetWeaponRecord = GetTargetWeaponRecord(targetWeaponRecords, selectConsumeInstanceWeapons);

                // 次に選択する武器がない場合は完了処理を実行
                if (nextTargetWeaponRecord == null)
                {
                    OnCompleteSelect(selectConsumeInstanceWeapons);
                }
                else
                {
                    self.InternalStartSelectConsumeInstanceWeapon(
                        actor,
                        nextTargetWeaponRecord,
                        targetWeaponRecords,
                        selectConsumeInstanceWeapons,
                        OnCompleteSelect,
                        OnCancel
                        );
                }
            }, () =>
            {
                OnCancel();
            }));
        }

        private static WeaponRecord GetTargetWeaponRecord(List<WeaponRecord> targetWeaponRecords, List<InstanceWeapon> selectConsumeInstanceWeapons)
        {
            foreach (var w in targetWeaponRecords)
            {
                var isTarget = true;
                foreach (var i in selectConsumeInstanceWeapons)
                {
                    if (w == i.WeaponRecord)
                    {
                        isTarget = false;
                        break;
                    }
                }

                if (isTarget)
                {
                    return w;
                }
            }

            return null;
        }
    }
}
