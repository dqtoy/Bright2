using System.Collections.Generic;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// ゲーム中に存在する全ての<see cref="Actor"/>を管理するクラス
    /// </summary>
    public sealed class ActorManager
    {
        private readonly List<Actor> players = new List<Actor>();

        private readonly List<Actor> enemies = new List<Actor>();
        
        private readonly CompositeDisposable streams = new CompositeDisposable();

        public ActorManager()
        {
            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var actor = x.Actor;
                    var tag = actor.tag;
                    if(tag == Tags.Name.Player)
                    {
                        _this.players.Add(actor);
                    }
                    else if(tag == Tags.Name.Enemy)
                    {
                        _this.enemies.Add(actor);
                    }
                    else
                    {
                        Assert.IsTrue(false, $"tag = {tag}は未定義の{typeof(Actor)}です");
                    }
                })
                .AddTo(this.streams);
        }

        ~ActorManager()
        {
            this.streams.Dispose();
        }
    }
}
