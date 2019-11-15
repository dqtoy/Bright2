using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// ユーザーの入力によって武器を作成するシーケンスをリクエストするメッセージ
    /// </summary>
    public sealed class RequestCreateWeaponUserInput : Message<RequestCreateWeaponUserInput, Actor>
    {
        /// <summary>
        /// 武器を作成したい<see cref="Actor"/>
        /// </summary>
        public Actor Actor => this.param1;
    }
}
