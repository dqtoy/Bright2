using System.Collections.Generic;
using HK.Bright2.Database;
using HK.Bright2.GameSystems;
using HK.Bright2.WeaponControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers.Messages
{
    /// <summary>
    /// 武器グリッドUIの表示をリクエストするメッセージ
    /// </summary>
    public sealed class RequestShowWeaponGridUI : Message<RequestShowWeaponGridUI, IReadOnlyList<InstanceWeapon>>
    {
        public IReadOnlyList<InstanceWeapon> Records => this.param1;
    }
}
