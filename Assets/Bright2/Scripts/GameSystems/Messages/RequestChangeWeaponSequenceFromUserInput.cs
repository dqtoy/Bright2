using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// ユーザーの入力によって武器を切り替えるシーケンスをリクエストするメッセージ
    /// </summary>
    public sealed class RequestChangeWeaponSequenceFromUserInput : Message<RequestChangeWeaponSequenceFromUserInput, Actor>
    {
        /// <summary>
        /// 武器を切り替えたい<see cref="Actor"/>
        /// </summary>
        public Actor Actor => this.param1;
    }
}
