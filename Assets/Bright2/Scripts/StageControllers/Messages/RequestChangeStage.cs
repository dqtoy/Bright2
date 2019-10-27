using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.StageControllers.Messages
{
    /// <summary>
    /// <see cref="Stage"/>の切り替えをリクエストするメッセージ
    /// </summary>
    public sealed class RequestChangeStage : Message<RequestChangeStage, Stage>
    {
        public Stage StagePrefab => this.param1;
    }
}
