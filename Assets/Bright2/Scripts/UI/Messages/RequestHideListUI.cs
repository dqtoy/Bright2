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
    /// リストUIの非表示をリクエストするメッセージ
    /// </summary>
    public sealed class RequestHideListUI : Message<RequestHideListUI>
    {
    }
}
