using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.Messages
{
    /// <summary>
    /// <see cref="Actor"/>がダメージを受けた際のメッセージ
    /// </summary>
    public sealed class TakedDamage : Message<TakedDamage, int, Vector2, Constants.DamageSource>
    {
        /// <summary>
        /// 受けたダメージ量
        /// </summary>
        public int Damage => this.param1;

        /// <summary>
        /// ダメージが発生した地点
        /// </summary>
        public Vector2 GenerationSource => this.param2;

        /// <summary>
        /// ダメージ発生源
        /// </summary>
        public Constants.DamageSource DamageSource => this.param3;
    }
}
