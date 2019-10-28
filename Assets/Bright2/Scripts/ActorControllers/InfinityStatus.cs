using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// 無敵のステータスを管理するクラス
    /// </summary>
    public sealed class InfinityStatus
    {
        private readonly Actor owner;

        /// <summary>
        /// 攻撃をしたオブジェクトのインスタンスID
        /// </summary>
        public int TargetInstanceId { get; private set; }

        /// <summary>
        /// 無敵時間（秒）
        /// </summary>
        public float InfinitySeconds { get; private set; }

        public InfinityStatus(Actor owner, int targetInstanceId, float inifinitySeconds)
        {
            this.owner = owner;
            this.TargetInstanceId = targetInstanceId;
            this.InfinitySeconds = inifinitySeconds;

            this.owner.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.InfinitySeconds -= Time.deltaTime;
                });
        }

        public void SetInfinitySeconds(float infinitySeconds)
        {
            this.InfinitySeconds = infinitySeconds;
        }
    }
}
