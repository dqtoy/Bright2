using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.InputSystems
{
    /// <summary>
    /// 入力されても何もしないクラス
    /// </summary>
    public sealed class NoneInput : IControllableUserInput
    {
        public static readonly NoneInput Default = new NoneInput();

        private NoneInput()
        {
        }

        void IControllableUserInput.UpdateInput()
        {
        }
    }
}
