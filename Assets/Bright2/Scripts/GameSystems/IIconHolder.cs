using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// アイコンを保持するインターフェイス
    /// </summary>
    public interface IIconHolder
    {
        Sprite Icon { get; }
    }
}
