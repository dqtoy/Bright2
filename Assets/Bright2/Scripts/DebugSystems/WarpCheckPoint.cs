using HK.Bright2.DebugSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

#if BRIGHT_DEBUG
namespace HK.Bright2.DebugSystems
{
    /// <summary>
    /// デバッグでワープ可能なチェックポイント
    /// </summary>
    public sealed class WarpCheckPoint : MonoBehaviour
    {
        [SerializeField]
        private int warpIndex = default;

        void Awake()
        {
            Broker.Global.Receive<RequestWarpCheckPoint>()
                .Where(x => x.WarpIndex == this.warpIndex)
                .SubscribeWithState(this, (x, _this) => x.WarpTarget.Movement.Warp(_this.transform.position))
                .AddTo(this);
        }
    }
}
#endif
