using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects;
using HK.Bright2.WeaponControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ダメージを与えるインターフェイス
    /// </summary>
    public interface IGiveDamage
    {
        /// <summary>
        /// ダメージ
        /// </summary>
        int DamagePower { get; }

        /// <summary>
        /// クリティカル確率
        /// </summary>
        float CriticalRate { get; }

        /// <summary>
        /// ノックバック
        /// </summary>
        float KnockbackPower { get; }

        /// <summary>
        /// 攻撃が当たった際に相手に付与する無敵時間
        /// </summary>
        float InfinitySeconds { get; }

        /// <summary>
        /// 武器タイプ
        /// </summary>
        Constants.WeaponType WeaponType { get; }

        /// <summary>
        /// ダメージを与える<see cref="Actor"/>
        /// </summary>
        Actor Owner { get; }

        /// <summary>
        /// ダメージを受けた<see cref="Actor"/>
        /// </summary>
        Actor Damager { get; }

        /// <summary>
        /// このオブジェクトを持つ親オブジェクト
        /// </summary>
        GameObject Root { get; }

        /// <summary>
        /// 実際にダメージを与える<see cref="GameObject"/>
        /// </summary>
        GameObject GiveDamageObject { get; }

        /// <summary>
        /// 実際にダメージを与える<see cref="Collider2D"/>
        /// </summary>
        /// <value></value>
        Collider2D GiveDamageCollider { get; }

        /// <summary>
        /// ダメージを与えられるタグリスト
        /// </summary>
        List<string> IncludeTags { get; }

        /// <summary>
        /// ノックバックの方向
        /// </summary>
        Vector2 KnockbackDirection { get; }

        /// <summary>
        /// 攻撃が貫通している回数
        /// </summary>
        /// <remarks>
        /// <c>-1</c>の場合は無限に貫通する
        /// </remarks>
        int CurrentPenetrationCount { get; set; }

        /// <summary>
        /// ダメージを与える<see cref="InstanceWeapon"/>
        /// </summary>
        /// <remarks>
        /// 装備していない場合は<c>null</c>が入ります
        /// </remarks>
        InstanceWeapon InstanceWeapon { get; set; }

        /// <summary>
        /// 攻撃が当たった際の追加効果
        /// </summary>
        List<GiveDamageActorAdditionalEffect> AdditionalEffects { get; }
    }
}
