using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// 状態異常がアタッチされた際のメッセージ
    /// </summary>
    public sealed class AttachedAbnormalCondition : Message<AttachedAbnormalCondition, Constants.AbnormalStatus>
    {
        /// <summary>
        /// アタッチされた状態異常のタイプ
        /// </summary>
        public Constants.AbnormalStatus Type => this.param1;
    }
}
