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

            if(giveDamage.CriticalRate.Lottery())
            {
                damage = Mathf.FloorToInt(damage * 1.5f);
            }

            if(attacker.AbnormalConditionController.Contains(Constants.AbnormalStatus.FireSpread))
            {
                damage = Mathf.FloorToInt(damage * 0.5f);
            }

            if(damage <= 0)
            {
                damage = 1;
            }

            return damage;
        }
    }
}
