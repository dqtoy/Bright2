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
            var attackerItemModifierEffect = attacker.StatusController.ItemModifierEffect;
            var targetItemModifierEffect = target.StatusController.ItemModifierEffect;
            var damage = giveDamage.DamagePower;

            // アクセサリーの効果分ダメージが上昇する
            var damageUpRate = attackerItemModifierEffect.GetPercent(Constants.ItemModifierType.GiveDamageUpRate);
            if(giveDamage.WeaponType == Constants.WeaponType.Melee)
            {
                damageUpRate += attackerItemModifierEffect.GetPercent(Constants.ItemModifierType.GiveDamageUpRateMeleeOnly);
            }
            var damageUp = 
                Mathf.FloorToInt(damage * damageUpRate) +
                attackerItemModifierEffect.Get(Constants.ItemModifierType.GiveDamageUpFixed);

            // 攻撃した武器に修飾がある場合はダメージが上昇する
            var instanceWeapon = giveDamage.InstanceWeapon;
            if(instanceWeapon != null)
            {
                var rateValue = 0.0f;
                var fixedValue = 0;
                foreach(var i in instanceWeapon.Modifiers)
                {
                    if(i.Type == Constants.ItemModifierType.WeaponDamageUpRate)
                    {
                        rateValue += i.Amount / 100.0f;
                    }
                    if(i.Type == Constants.ItemModifierType.WeaponDamageUpFixed)
                    {
                        fixedValue += i.Amount;
                    }
                }

                damageUp += Mathf.FloorToInt(damage * rateValue) + fixedValue;
            }

            // アクセサリーの効果分ダメージが減少する
            var damageDownRate = targetItemModifierEffect.GetPercent(Constants.ItemModifierType.TakeDamageDownRate);
            var damageDown =
                Mathf.FloorToInt(damage * damageDownRate) +
                targetItemModifierEffect.Get(Constants.ItemModifierType.TakeDamageDownFixed);

            damage = damage + damageUp - damageDown;

            // クリティカルヒットの場合はダメージが上昇
            var criticalRate = giveDamage.CriticalRate + attackerItemModifierEffect.GetPercent(Constants.ItemModifierType.CriticalUpRate);
            var isCritical = criticalRate.Lottery();
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

        /// <summary>
        /// 酸欠ダメージを返す
        /// </summary>
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

        /// <summary>
        /// 毒ダメージを返す
        /// </summary>
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

        /// <summary>
        /// ノックバック量を返す
        /// </summary>
        public static Vector2 GetKnockbackPower(Vector2 direction, float power, float resistance, float limit)
        {
            if(power <= resistance)
            {
                return Vector2.zero;
            }

            var finalPower = Mathf.Min(Mathf.Pow(power - resistance, 0.95f), limit);

            return direction.normalized * finalPower;
        }

        /// <summary>
        /// 与えたダメージ値から回復量を返す
        /// </summary>
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

        /// <summary>
        /// お金を返す
        /// </summary>
        public static int GetMoney(Actor attacker, int money)
        {
            if(attacker == null)
            {
                return money;
            }

            var dropRate = attacker.StatusController.ItemModifierEffect.GetPercent(Constants.ItemModifierType.DropMoneyUpRate);
            if(dropRate <= 0.0f)
            {
                return money;
            }

            var addMoney = Mathf.FloorToInt(money * dropRate);
            addMoney = Mathf.Max(addMoney, 1);

            return money + addMoney;
        }
    }
}
