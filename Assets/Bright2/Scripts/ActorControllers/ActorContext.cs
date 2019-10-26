using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>を構成するのに必要なデータを持つクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/ActorContext")]
    public sealed class ActorContext : ScriptableObject
    {
        [SerializeField]
        private ActorAnimationSequence idle = default;
        public ActorAnimationSequence Idle => this.idle;

        [SerializeField]
        private ActorAnimationSequence run = default;
        public ActorAnimationSequence Run => this.run;
    }
}
