using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.InputSystems
{
    /// <summary>
    /// 入力されても何もしないクラス
    /// </summary>
    public sealed class NoneInput : IControllableUserInput
    {
        void IControllableUserInput.UpdateInput()
        {
        }
    }
}
