using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// オブジェクトプール可能なインターフェイス
    /// </summary>
    public interface IPoolableComponent
    {
        void Return();
    }
}
