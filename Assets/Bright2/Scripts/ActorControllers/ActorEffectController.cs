using HK.Bright2.ActorControllers.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>に対して様々なエフェクトを生成するクラス
    /// </summary>
    public sealed class ActorEffectController
    {
        private readonly Actor owner;

        public ActorEffectController(Actor owner)
        {
            this.owner = owner;

            this.owner.Broker.Receive<TakedDamage>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var effect = _this.owner.Context.Effects.TakedDamage.Rent();
                    effect.transform.position = x.Result.GenerationSource;
                })
                .AddTo(this.owner);
        }
    }
}
