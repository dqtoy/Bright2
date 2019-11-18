using HK.Bright2.ActorControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// <see cref="ChangeAccessoryUserInput"/>に関するメッセージ
    /// </summary>
    public sealed class ChangeAccessoryUserInputMessages
    {
        /// <summary>
        /// アクセサリー切り替えの開始をリクエストするメッセージ
        /// </summary>
        public sealed class Request : Message<Request, Actor>
        {
            /// <summary>
            /// アクセサリーを切り替えたい<see cref="Actor"/>
            /// </summary>
            public Actor Actor => this.param1;
        }

        /// <summary>
        /// アクセサリー切り替え終了のメッセージ
        /// </summary>
        public sealed class End : Message<End>
        {
        }
    }
}
