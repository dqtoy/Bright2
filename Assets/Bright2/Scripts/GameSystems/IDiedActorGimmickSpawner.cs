using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GimmickControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// <see cref="Actor"/>が死亡した際にギミックを生成するインターフェイス
    /// </summary>
    public interface IDiedActorGimmickSpawner<T, E> where T : IDropItem<E>
    {
        /// <summary>
        /// 生成するギミックのプレハブ
        /// </summary>
        Gimmick GimmickPrefab { get; }

        Vector3 GetSpawnPoint(Actor actor);

        IEnumerable<T> GetDropData(Actor actor);

        void Setup(Gimmick gimmick, E dropData);

        /// <summary>
        /// アイテムをドロップするか抽選を行う
        /// </summary>
        bool Lottery(T dropItem, Actor attacker);
    }
}
