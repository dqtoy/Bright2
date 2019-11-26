using System;
using HK.Bright2.Database;
using HK.Bright2.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// 大事なアイテムのドロップデータ
    /// </summary>
    [Serializable]
    public sealed class DropImportantItem : DropItem<ImportantItemRecord>
    {
    }
}
