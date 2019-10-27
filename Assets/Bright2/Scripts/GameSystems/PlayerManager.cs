using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// プレイヤーを管理するクラス
    /// </summary>
    public sealed class PlayerManager
    {
        private readonly List<Actor> players = new List<Actor>();
        
        private readonly CompositeDisposable streams = new CompositeDisposable();

        public PlayerManager()
        {
            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var actor = x.Actor;
                    if(actor.tag == Tags.Name.Player)
                    {
                        _this.players.Add(actor);
                    }
                })
                .AddTo(this.streams);
        }

        ~PlayerManager()
        {
            this.streams.Dispose();
        }
    }
}
