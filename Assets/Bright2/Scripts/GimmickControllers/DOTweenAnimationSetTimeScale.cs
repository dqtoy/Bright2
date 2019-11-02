using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

namespace HK.Bright2.GimmickControllers
{
    /// <summary>
    /// 攻撃速度の上昇の影響を受けて<see cref="DOTweenAnimation"/>のタイムスケールを設定するクラス
    /// </summary>
    public sealed class DOTweenAnimationSetTimeScale : IAffectedSpeedUp
    {
        private readonly DOTweenAnimation[] tweenAnimations;

        public DOTweenAnimationSetTimeScale(GameObject root)
        {
            this.tweenAnimations = root.GetComponentsInChildren<DOTweenAnimation>();
        }

        void IAffectedSpeedUp.Affected(float rate)
        {
            foreach(var t in this.tweenAnimations)
            {
                t.tween.timeScale = 1.0f + rate;
            }
        }
    }
}
