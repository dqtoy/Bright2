using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers
{
    /// <summary>
    /// <see cref="Gimmick"/>のライフサイクルを制御するクラス
    /// </summary>
    public sealed class GimmickLifeCycleController
    {
        private readonly Gimmick owner;

        public GimmickLifeCycleController(Gimmick owner)
        {
            this.owner = owner;
            
            Broker.Global.Receive<ChangedStage>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.owner.Return();
                })
                .AddTo(this.owner);
        }
    }
}
