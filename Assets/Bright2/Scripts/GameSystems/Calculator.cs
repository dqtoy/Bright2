using HK.Bright2.ActorControllers;
using HK.Bright2.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ゲームで使用する計算式をまとめたクラス
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// <see cref="Actor"/>と<see cref="Actor"/>とのダメージ計算をして返す
        /// </summary>
        public static DamageResult GetDamageOnActor(IGiveDamage giveDamage, Actor target, Vector2 generationSource)
        {
            var attacker = giveDamage.Owner;
            var attackerAccessoryEffect = attacker.StatusController.ItemModifierEffect;
            var targetAccessoryEffect = target.StatusController.ItemModifierEffect;
            var damage = giveDamage.DamagePower;

            // アクセサリーの効果分ダメージが上昇する
            var damageUpRate = attacker.StatusController.ItemModifierEffect.GetPercent(Constants.ItemModifierType.GiveDamageUpRate);
            if(giveDamage.WeaponType == Constants.WeaponType.Melee)
            {
                damageUpRate += attacker.StatusController.ItemModifierEffect.GetPercent(Constants.ItemModifierType.GiveDamageUpRateMeleeOnly);
            }
            var damageUp = Mathf.FloorToInt(damage * damageUpRate);

            // アクセサリーの効果分ダメージが減少する
            var damageDown = Mathf.FloorToInt(damage * target.StatusController.ItemModifierEffect.GetPercent(Constants.ItemModifierType.TakeDamageDownRate));

            damage = damage + damageUp - damageDown;

            // クリティカルヒットの場合はダメージが上昇
            var isCritical = giveDamage.CriticalRate.Lottery();
            if(isCritical)
            {
                damage = Mathf.FloorToInt(damage * 1.5f);
            }

            // 延焼の状態異常にかかっている場合は半減する
            if(attacker.AbnormalConditionController.Contains(Constants.AbnormalStatus.FireSpread))
            {
                damage = Mathf.FloorToInt(damage * 0.5f);
            }

            // 詰み防止
            if(damage <= 0)
            {
                damage = 1;
            }

            return new DamageResult(
                attacker,
                target,
                damage,
                generationSource,
                Constants.DamageSource.Actor,
                isCritical
            );
        }

        public static DamageResult GetDamageResultOnLackOfOxygen(Actor target)
        {
            var damage = Mathf.FloorToInt(target.StatusController.HitPointMax.Value * Constants.LackOfOxygenDamageRate);
            return new DamageResult(
                null,
                target,
                damage,
                target.CachedTransform.position,
                Constants.DamageSource.LackOfOxygen,
                false
            );
        }

        public static DamageResult GetDamageResultOnPoison(Actor target, ActorContext.AbnormalConditionParameters.PoisonParameter poisonContext, int damageSplitCount)
        {
            var damage = Mathf.FloorToInt((poisonContext.DamageRate * target.StatusController.HitPointMax.Value) / damageSplitCount);
            return new DamageResult(
                null,
                target,
                damage,
                target.CachedTransform.position,
                Constants.DamageSource.AbnormalStatus,
                false
            );
        }

        public static DamageResult GetRecoveryFromDamage(Actor target, int damage, float recoveryRate)
        {
            var recoveryValue = Mathf.FloorToInt((float)damage * recoveryRate);
            if(recoveryValue <= 0)
            {
                recoveryValue = 1;
            }
            
            return new DamageResult(
                target,
                target,
                -recoveryValue,
                target.CachedTransform.position,
                Constants.DamageSource.Actor,
                false
            );
        }

        /// <summary>
        /// 倍率をかけた攻撃速度を返す
        /// </summary>
        public static float GetFireSpeedUp(float origin, float rate)
        {
            var result = origin;
            result -= origin * rate;
            var min = (float)(2.0f / Constants.TargetFrameRate);

            // 秒間の更新時間より下回ったら攻撃判定が発生しないので丸め込む
            if(result < min)
            {
                result = min;
            }

            return result;
        }
    }
}
