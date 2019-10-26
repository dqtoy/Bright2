using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.Extensions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.States
{
    /// <summary>
    /// 攻撃状態を制御するクラス
    /// </summary>
    public sealed class Attack : ActorState
    {
        private float nextStateDelaySeconds;

        public Attack(Actor owner)
            :base(owner)
        {
        }

        public override void Enter(IActorStateContext context)
        {
            base.Enter(context);

            this.nextStateDelaySeconds = 0.0f;
            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Attack);

            var gimmick = this.owner.StatusController.Equipment.Gimmick.Rent();
            var parent = this.owner.TransformHolder.GetEquipmentOrigin(this.owner.StatusController.Direction);
            gimmick.transform.position = parent.position;
            gimmick.transform.rotation = parent.rotation;
            gimmick.Activate(this.owner);

            this.owner.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.nextStateDelaySeconds += Time.deltaTime;
                    if (_this.nextStateDelaySeconds >= _this.owner.StatusController.Equipment.NextStateDelaySeconds)
                    {
                        _this.owner.StateManager.Change(ActorState.Name.Idle);
                    }
                })
                .AddTo(this.events);
        }
    }
}
