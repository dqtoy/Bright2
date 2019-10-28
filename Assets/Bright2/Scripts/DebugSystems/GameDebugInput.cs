using HK.Bright2.ActorControllers;
using HK.Bright2.Database;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.DebugSystems
{
    /// <summary>
    /// ゲームデバッグする入力を制御するクラス
    /// </summary>
    public sealed class GameDebugInput : MonoBehaviour
    {
        [SerializeField]
        private WeaponRecord weapon = default;

        private Actor player;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.player = x.Actor;
                })
                .AddTo(this);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.player.StatusController.AddWeapon(this.weapon);
            }
        }
    }
}
