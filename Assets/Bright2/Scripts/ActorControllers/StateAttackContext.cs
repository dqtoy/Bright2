using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers.States;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="ActorState.Name.Attack"/>ステートで必要なデータを持つクラス
    /// </summary>
    public sealed class StateAttackContext : IActorStateContext
    {
        /// <summary>
        /// 攻撃したい装備中の装備品のインデックス
        /// </summary>
        public int EquippedWeaponIndex { get; }

        public StateAttackContext(int equippedWeaponIndex)
        {
            this.EquippedWeaponIndex = equippedWeaponIndex;
        }
    }
}
