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

        private bool canNextFire = false;

        public Attack(Actor owner)
            :base(owner)
        {
        }

        public override void Enter(IActorStateContext context)
        {
            base.Enter(context);

            var stateAttackContext = (StateAttackContext)context;
            Assert.IsNotNull(stateAttackContext);

            var equippedWeapon = this.owner.StatusController.EquippedWeapons[stateAttackContext.EquippedWeaponIndex];
            Assert.IsTrue(equippedWeapon.CanFire, "攻撃不可能な状態なのに攻撃しようとしました");

            this.nextStateDelaySeconds = 0.0f;
            this.owner.AnimationController.StartSequence(this.owner.Context.AnimationSequences.Attack);
            var weaponRecord = equippedWeapon.InstanceWeapon.WeaponRecord;
            this.canNextFire = weaponRecord.CanAutoAttack;

            this.CreateGimmick(equippedWeapon);

            if(this.canNextFire)
            {
                this.owner.Broker.Receive<RequestTerminationFire>()
                    .Where(x => x.EquippedWeaponIndex == stateAttackContext.EquippedWeaponIndex)
                    .SubscribeWithState(this, (_, _this) =>
                    {
                        _this.canNextFire = false;
                    })
                    .AddTo(this.events);
            }

            // 恐怖の状態異常にかかったらオート攻撃を終了する
            this.owner.Broker.Receive<AttachedAbnormalCondition>()
                .Where(x => x.Type == Constants.AbnormalStatus.Fear)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.canNextFire = false;
                })
                .AddTo(this.events);

            this.owner.UpdateAsObservable()
                .SubscribeWithState3(this, weaponRecord, equippedWeapon, (_, _this, _weaponRecord, _equippedWeapon) =>
                {
                    _this.nextStateDelaySeconds += Time.deltaTime;
                    if (_this.nextStateDelaySeconds >= _weaponRecord.NextStateDelaySeconds)
                    {
                        if(this.canNextFire)
                        {
                            _this.CreateGimmick(_equippedWeapon);
                            _this.nextStateDelaySeconds = 0.0f;
                        }
                        else
                        {
                            _this.owner.StateManager.Change(ActorState.Name.Idle);
                        }
                    }
                })
                .AddTo(this.events);

            var moveSpeed = this.owner.Context.BasicStatus.MoveSpeed;
            moveSpeed -= moveSpeed * weaponRecord.MoveSpeedAttenuationRate;
            this.ReceiveRequestMoveOnMove(moveSpeed);
        }

        private void CreateGimmick(EquippedWeapon equippedWeapon)
        {
            var weaponRecord = equippedWeapon.InstanceWeapon.WeaponRecord;
            var gimmick = weaponRecord.Gimmick.Rent();
            var parent = this.owner.TransformHolder.GetWeaponOrigin(this.owner.StatusController.Direction);
            gimmick.transform.position = parent.position;
            gimmick.transform.rotation = parent.rotation;
            gimmick.Activate(this.owner);
            equippedWeapon.ResetCoolTime();
        }
    }
}
