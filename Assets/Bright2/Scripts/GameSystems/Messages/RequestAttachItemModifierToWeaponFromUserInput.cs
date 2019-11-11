using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// ユーザーの入力によって武器にアイテム修飾を装着するのをリクエストするメッセージ
    /// </summary>
    public sealed class RequestAttachItemModifierToWeaponFromUserInput : Message<RequestAttachItemModifierToWeaponFromUserInput, Actor>
    {
        /// <summary>
        /// アイテム修飾を装着したい<see cref="Actor"/>
        /// </summary>
        public Actor Actor => this.param1;
    }
}
