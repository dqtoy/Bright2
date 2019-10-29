using System;
using DG.Tweening;
using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DOTweenAnimationPositionFlip : MonoBehaviour, IGimmickDecorator
    {
        [SerializeField]
        private DOTweenAnimation target = default;

        [SerializeField]
        private FlipFlag flipFlag = default;

        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            // 基本は右向きでギミックを作成しているので左を向いていた場合は値をひっくり返す
            var value = this.target.endValueV3;
            if(this.flipFlag.X && owner.Direction == Constants.Direction.Left)
            {
                value.x = -value.x;
            }
            if(this.flipFlag.Y && owner.Direction == Constants.Direction.Left)
            {
                value.y = -value.y;
            }
            if(this.flipFlag.Z && owner.Direction == Constants.Direction.Left)
            {
                value.z = -value.z;
            }

            this.target.endValueV3 = value;
        }

        [Serializable]
        public class FlipFlag
        {
            [SerializeField]
            private bool x = default, y = default, z = default;

            public bool X => this.x;

            public bool Y => this.y;

            public bool Z => this.z;
        }
    }
}
