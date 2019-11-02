using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ダメージ結果を格納する
    /// </summary>
    public struct DamageResult
    {
        public Actor Attacker { get; }

        public Actor Damager { get; }

        public int Damage { get; }

        /// <summary>
        /// ダメージが発生した座標
        /// </summary>
        public Vector2 GenerationSource { get; }

        public Constants.DamageSource DamageSource { get; }

        public bool IsCritical { get; }

        public bool IsRecovery => this.Damage < 0;

        public DamageResult(Actor attacker, Actor damager, int damage, Vector2 generationSource, Constants.DamageSource damageSource, bool isCritical)
        {
            this.Attacker = attacker;
            this.Damager = damager;
            this.Damage = damage;
            this.GenerationSource = generationSource;
            this.DamageSource = damageSource;
            this.IsCritical = isCritical;
        }
    }
}
