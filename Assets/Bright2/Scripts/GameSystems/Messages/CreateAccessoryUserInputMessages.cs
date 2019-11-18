using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// <see cref="CreateAccessoryUserInput"/>に関するメッセージ
    /// </summary>
    public sealed class CreateAccessoryUserInputMessages
    {
        /// <summary>
        /// アクセサリー作成の開始をリクエストするメッセージ
        /// </summary>
        public sealed class Request : Message<Request, Actor>
        {
            /// <summary>
            /// アクセサリー作成する<see cref="Actor"/>
            /// </summary>
            public Actor Actor => this.param1;
        }

        /// <summary>
        /// アクセサリー作成終了のメッセージ
        /// </summary>
        public sealed class End : Message<End>
        {
        }
    }
}
