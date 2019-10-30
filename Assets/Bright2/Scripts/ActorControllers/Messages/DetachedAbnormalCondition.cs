using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// 状態異常がデタッチされた際のメッセージ
    /// </summary>
    public sealed class DetachedAbnormalCondition : Message<DetachedAbnormalCondition, Constants.AbnormalStatus>
    {
        /// <summary>
        /// デタッチされた状態異常のタイプ
        /// </summary>
        public Constants.AbnormalStatus Type => this.param1;
    }
}
