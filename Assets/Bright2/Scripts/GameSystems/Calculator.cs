﻿using HK.Bright2.ActorControllers;
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
            var attackerAccessoryEffect = attacker.StatusController.AccessoryEffect;
            var targetAccessoryEffect = target.StatusController.AccessoryEffect;
            var damage = giveDamage.DamagePower;

            // アクセサリーの効果分ダメージが上昇する
            var damageUpRate = attacker.StatusController.AccessoryEffect.DamageUp;
            if(giveDamage.WeaponType == Constants.WeaponType.Melee)
            {
                damageUpRate += attacker.StatusController.AccessoryEffect.DamageUpMeleeOnly;
            }
            var damageUp = Mathf.FloorToInt(damage * damageUpRate);

            // アクセサリーの効果分ダメージが減少する
            var damageDown = Mathf.FloorToInt(damage * target.StatusController.AccessoryEffect.DamageDown);

            damage = damage + damageUp - damageDown;

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
