using System;
using System.Collections.Generic;
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
        private AnimationSequenceElements animationSequences = default;
        public AnimationSequenceElements AnimationSequences => this.animationSequences;

        [SerializeField]
        private BasicStatusElement basicStatus = default;
        public BasicStatusElement BasicStatus => this.basicStatus;

        [Serializable]
        public class AnimationSequenceElements
        {
            [SerializeField]
            private ActorAnimationSequence idle = default;
            public ActorAnimationSequence Idle => this.idle;

            [SerializeField]
            private ActorAnimationSequence run = default;
            public ActorAnimationSequence Run => this.run;

            [SerializeField]
            private ActorAnimationSequence jump = default;
            public ActorAnimationSequence Jump => this.jump;

            [SerializeField]
            private ActorAnimationSequence fall = default;
            public ActorAnimationSequence Fall => this.fall;

            [SerializeField]
            private ActorAnimationSequence attack = default;
            public ActorAnimationSequence Attack => this.attack;
        }

        [Serializable]
        public class BasicStatusElement
        {
            [SerializeField]
            private int hitPoint = default;
            public int HitPoint => this.hitPoint;
            
            [SerializeField]
            private float moveSpeed = default;
            public float MoveSpeed => this.moveSpeed;
            
            [SerializeField]
            private float jumpPower = default;
            public float JumpPower => this.jumpPower;

            [SerializeField]
            private int limitJumpCount = default;
            /// <summary>
            /// 連続でジャンプできる回数
            /// </summary>
            public int LimitJumpCount => this.limitJumpCount;

            [SerializeField]
            private int money = default;
            public int Money => this.money;

            [SerializeField]
            private List<DropEquipment> dropEquipments = default;
            public List<DropEquipment> DropEquipments => this.dropEquipments;
        }
    }
}
