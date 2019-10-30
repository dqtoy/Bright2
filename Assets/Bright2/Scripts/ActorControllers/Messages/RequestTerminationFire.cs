using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// 攻撃の終了をリクエストするメッセージ
    /// </summary>
    public sealed class RequestTerminationFire : Message<RequestTerminationFire, int>
    {
        /// <summary>
        /// 攻撃を終了したい装備中の武器のインデックス
        /// </summary>
        public int EquippedWeaponIndex => this.param1;
    }
}
