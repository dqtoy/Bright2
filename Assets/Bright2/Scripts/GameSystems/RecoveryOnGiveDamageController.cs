using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// 与えたダメージから回復を行うクラス
    /// </summary>
    public sealed class RecoveryOnGiveDamageController : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveActor(x.Actor);
                })
                .AddTo(this);
        }

        private void ObserveActor(Actor actor)
        {
            actor.Broker.Receive<GivedDamage>()
                .Where(x => x.Result.Attacker.StatusController.AccessoryEffect.RecoveryOnGiveDamage > 0.0f)
                .Where(x => x.Result.Damage > 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var damageResult = Calculator.GetRecoveryFromDamage(x.Result.Attacker, x.Result.Damage, x.Result.Attacker.StatusController.AccessoryEffect.RecoveryOnGiveDamage);
                    x.Result.Attacker.StatusController.TakeDamage(damageResult);
                })
                .AddTo(this);
        }
    }
}
