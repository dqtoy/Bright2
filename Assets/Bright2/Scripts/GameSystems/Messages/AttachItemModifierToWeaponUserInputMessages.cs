using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// <see cref="AttachItemModifierToWeaponUserInput"/>に関するメッセージ
    /// </summary>
    public sealed class AttachItemModifierToWeaponUserInputMessages
    {
        /// <summary>
        /// 武器切り替えの開始をリクエストするメッセージ
        /// </summary>
        public sealed class Request : Message<Request, Actor>
        {
            /// <summary>
            /// 武器を切り替えたい<see cref="Actor"/>
            /// </summary>
            public Actor Actor => this.param1;
        }

        /// <summary>
        /// 武器切り替え終了のメッセージ
        /// </summary>
        public sealed class End : Message<End>
        {
        }
    }
}
