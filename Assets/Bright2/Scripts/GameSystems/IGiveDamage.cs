using System.Collections.Generic;
using HK.Bright2.ActorControllers;
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
        /// ノックバック
        /// </summary>
        float KnockbackPower { get; }

        /// <summary>
        /// 攻撃が当たった際に相手に付与する無敵時間
        /// </summary>
        float InfinitySeconds { get; }

        /// <summary>
        /// ダメージを与える<see cref="Actor"/>
        /// </summary>
        Actor Owner { get; }

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
    }
}
