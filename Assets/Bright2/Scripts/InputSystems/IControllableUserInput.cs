using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.InputSystems
{
    /// <summary>
    /// ユーザーの入力に対して様々な処理を行うインターフェイス
    /// </summary>
    public interface IControllableUserInput
    {
        void UpdateInput();
    }
}
