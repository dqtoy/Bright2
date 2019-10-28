using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>のライフサイクルを制御するクラス
    /// </summary>
    public sealed class ActorLifeCycleController
    {
        private readonly Actor owner;

        public ActorLifeCycleController(Actor owner)
        {
            this.owner = owner;

            this.owner.Broker.Receive<Died>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    Object.Destroy(_this.owner.gameObject);
                })
                .AddTo(this.owner);

            if(this.owner.tag == Tags.Name.Enemy)
            {
                Broker.Global.Receive<RequestChangeStage>()
                    .SubscribeWithState(this, (_, _this) =>
                    {
                        Object.Destroy(_this.owner.gameObject);
                    })
                    .AddTo(this.owner);
            }
        }
    }
}
