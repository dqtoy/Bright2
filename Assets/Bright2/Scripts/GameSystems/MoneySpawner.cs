using System.Collections.Generic;
using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.GimmickControllers;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// お金オブジェクトを生成するクラス
    /// </summary>
    public sealed class MoneySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Gimmick> coinPrefabs = default;

        [SerializeField]
        private Vector3 offset = default;

        [SerializeField]
        private Vector3 random = default;

        void Awake()
        {
            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveActor(x.Actor);
                })
                .AddTo(this);
        }

        private void ObserveActor(Actor actor)
        {
            actor.Broker.Receive<Died>()
                .SubscribeWithState2(this, actor, (x, _this, a) =>
                {
                    _this.CreateGimmick(a, a.StatusController.Money);
                })
                .AddTo(this);
        }

        private void CreateGimmick(Actor actor, int money)
        {
            int digit = 0;
            while(money > 0)
            {
                var number = this.GetCreateNumber(digit, money % 10);
                var prefab = this.GetCoinPrefab(digit);

                for (var i = 0; i < number; i++)
                {
                    var coin = prefab.Rent();
                    var random = new Vector3(
                        Random.Range(-this.random.x, this.random.x),
                        Random.Range(0.0f, this.random.y),
                        Random.Range(-this.random.z, this.random.z)
                    );
                    coin.transform.position = actor.CachedTransform.position + this.offset + random;
                    coin.transform.rotation = Quaternion.identity;
                    coin.Activate(actor);
                }


                money /= 10;
                digit++;
            }
        }

        private Gimmick GetCoinPrefab(int index)
        {
            // 配列を超えていた場合は最後のプレハブを返す
            if(this.coinPrefabs.Count <= index)
            {
                return this.coinPrefabs[this.coinPrefabs.Count - 1];
            }

            return this.coinPrefabs[index];
        }

        private int GetCreateNumber(int index, int number)
        {
            if(this.coinPrefabs.Count <= index)
            {
                var digit = 1 + (this.coinPrefabs.Count - index);
                for (var i = 0; i < digit; i++)
                {
                    number *= 10;
                }

                return number;
            }

            return number;
        }
    }
}
