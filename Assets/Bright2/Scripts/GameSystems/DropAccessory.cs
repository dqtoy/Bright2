using System;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// アクセサリーのドロップデータ
    /// </summary>
    [Serializable]
    public sealed class DropAccessory : DropItem<AccessoryRecord>
    {
    }
}
