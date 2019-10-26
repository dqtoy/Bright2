using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2
{
    /// <summary>
    /// <see cref="IMessageBroker"/>を保持するインターフェイス
    /// </summary>
    public interface IBroker
    {
        IMessageBroker Broker { get; }
    }
}
