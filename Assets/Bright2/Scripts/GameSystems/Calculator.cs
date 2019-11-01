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
        public static int GetDamage(IGiveDamage giveDamage, Actor target)
        {
            var attacker = giveDamage.Owner;
            var damage = giveDamage.DamagePower;

            // アクセサリーの効果分ダメージが上昇する
            damage += Mathf.FloorToInt(damage * attacker.StatusController.AccessoryEffect.DamageUp);

            // クリティカルヒットの場合はダメージが上昇
            if(giveDamage.CriticalRate.Lottery())
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

            return damage;
        }
    }
}
