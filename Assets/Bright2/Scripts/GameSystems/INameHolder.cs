using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// 名前を保持するインターフェイス
    /// </summary>
    public interface INameHolder
    {
        string Name { get; }
    }
}
