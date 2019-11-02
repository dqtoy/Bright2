using HK.Bright2.GameSystems;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// <see cref="Actor"/>がダメージを受けた際のメッセージ
    /// </summary>
    public sealed class TakedDamage : Message<TakedDamage, DamageResult>
    {
        /// <summary>
        /// ダメージ結果
        /// </summary>
        public DamageResult Result => this.param1;
    }
}
