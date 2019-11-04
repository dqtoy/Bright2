using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.Messages
{
    /// <summary>
    /// 武器の生成をリクエストするメッセージ
    /// </summary>
    public sealed class RequestSpawnWeapon : Message<RequestSpawnWeapon, Actor, WeaponRecord>
    {
        public Actor Owner => this.param1;

        public WeaponRecord WeaponRecord => this.param2;
    }
}
