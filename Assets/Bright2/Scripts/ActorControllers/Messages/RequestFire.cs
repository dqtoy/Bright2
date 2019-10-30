using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// <see cref="Actor"/>に対して攻撃をリクエストするメッセージ
    /// </summary>
    public sealed class RequestFire : Message<RequestFire, int>
    {
        /// <summary>
        /// 攻撃したい装備中の装備品のインデックス
        /// </summary>
        public int EquippedWeaponIndex => this.param1;
    }
}
