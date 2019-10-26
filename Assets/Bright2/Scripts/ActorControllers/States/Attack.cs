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
            var equipmentRecord = this.owner.StatusController.EquippedEquipment.EquipmentRecord;
            var gimmick = equipmentRecord.Gimmick.Rent();
            var parent = this.owner.TransformHolder.GetEquipmentOrigin(this.owner.StatusController.Direction);
            gimmick.transform.position = parent.position;
            gimmick.transform.rotation = parent.rotation;
            gimmick.Activate(this.owner);
            this.owner.StatusController.EquippedEquipment.ResetCoolTime();

            this.owner.UpdateAsObservable()
                .SubscribeWithState2(this, equipmentRecord, (_, _this, _equipmentRecord) =>
                {
                    _this.nextStateDelaySeconds += Time.deltaTime;
                    if (_this.nextStateDelaySeconds >= _equipmentRecord.NextStateDelaySeconds)
                    {
                        _this.owner.StateManager.Change(ActorState.Name.Idle);
                    }
                })
                .AddTo(this.events);

            var moveSpeed = this.owner.Context.BasicStatus.MoveSpeed;
            moveSpeed -= moveSpeed * equipmentRecord.MoveSpeedAttenuationRate;
            this.ReceiveRequestMoveOnMove(moveSpeed);
        }
    }
}
