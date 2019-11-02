using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers
{
    /// <summary>
    /// アクセサリーの攻撃速度が上昇する効果を受けるインターフェイス
    /// </summary>
    public interface IAffectedSpeedUp
    {
        /// <summary>
        /// 攻撃速度上昇を適用する
        /// </summary>
        void Affected(float rate);
    }
}
