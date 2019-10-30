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

        [SerializeField]
        private EffectElements effects = default;
        public EffectElements Effects => this.effects;

        [SerializeField]
        private AbnormalConditionParameters abnormalCondition = default;
        public AbnormalConditionParameters AbnormalCondition => this.abnormalCondition;

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
            private List<DropWeapon> dropWeapons = default;
            public List<DropWeapon> DropWeapons => this.dropWeapons;
        }

        [Serializable]
        public class EffectElements
        {
            [SerializeField]
            private PoolableEffect takedDamage = default;
            public PoolableEffect TakedDamage => this.takedDamage;
        }

        [Serializable]
        public class AbnormalConditionParameters
        {
            [SerializeField]
            private PoisonParameter poison = default;
            /// <summary>
            /// 毒に関するパラメータ
            /// </summary>
            public PoisonParameter Poison => this.poison;

            [SerializeField]
            private ParalysisParameter paralysis = default;
            /// <summary>
            /// 麻痺に関するパラメータ
            /// </summary>
            public ParalysisParameter Paralysis => this.paralysis;

            [SerializeField]
            private ConfuseParameter confuse = default;
            /// <summary>
            /// 混乱に関するパラメータ
            /// </summary>
            public ConfuseParameter Confuse => this.confuse;

            [SerializeField]
            private FearParameter fear = default;
            /// <summary>
            /// 恐怖に関するパラメータ
            /// </summary>
            public FearParameter Fear => this.fear;

            [SerializeField]
            private PoisonParameter deadlyPoison = default;
            /// <summary>
            /// 猛毒に関するパラメータ
            /// </summary>
            public PoisonParameter DeadlyPoison => this.deadlyPoison;

            [SerializeField]
            private PoisonParameter fireSpread = default;
            /// <summary>
            /// 延焼に関するパラメータ
            /// </summary>
            public PoisonParameter FireSpread => this.fireSpread;

            [Serializable]
            public class PoisonParameter
            {
                [SerializeField]
                private float duration = default;
                /// <summary>
                /// 毒にかかる時間（秒）
                /// </summary>
                public float Duration => this.duration;

                [SerializeField][Range(0.0f, 1.0f)]
                private float damageRate = default;
                /// <summary>
                /// トータルで受けるダメージ
                /// </summary>
                public float DamageRate => this.damageRate;
            }

            [Serializable]
            public class ParalysisParameter
            {
                [SerializeField]
                private float duration = default;
                /// <summary>
                /// 行動が出来ない時間（秒）
                /// </summary>
                public float Duration => this.duration;
            }

            [Serializable]
            public class ConfuseParameter
            {
                [SerializeField]
                private float duration = default;
                /// <summary>
                /// 移動が逆転してしまう時間（秒）
                /// </summary>
                public float Duration => this.duration;
            }

            [Serializable]
            public class FearParameter
            {
                [SerializeField]
                private float duration = default;
                /// <summary>
                /// 攻撃が出来なくなる時間（秒）
                /// </summary>
                public float Duration => this.duration;
            }
        }
    }
}
