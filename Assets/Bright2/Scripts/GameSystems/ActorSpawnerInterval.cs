using System;
using HK.Bright2.ActorControllers;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// 一定間隔で<see cref="Actor"/>を生成するクラス
    /// </summary>
    public sealed class ActorSpawnerInterval : MonoBehaviour
    {
        [SerializeField]
        private Actor prefab = default;

        [SerializeField]
        private int limit = default;

        [SerializeField]
        private float interval = default;

        private int count = 0;

        private float duration = 0.0f;

        void Update()
        {
            if(!this.CanUpdate)
            {
                return;
            }

            this.duration += Time.deltaTime;

            if(this.duration >= this.interval)
            {
                this.prefab.Spawn(this.transform.position);
                this.duration = 0.0f;
                this.count++;
            }
        }

        private bool CanUpdate
        {
            get
            {
                if(this.limit == -1)
                {
                    return true;
                }

                return this.count <= this.limit;
            }
        }
    }
}
