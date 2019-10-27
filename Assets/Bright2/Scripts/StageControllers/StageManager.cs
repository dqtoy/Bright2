using HK.Bright2.StageControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// <see cref="Stage"/>を管理するクラス
    /// </summary>
    public sealed class StageManager
    {
        private Stage current;

        private readonly CompositeDisposable events = new CompositeDisposable();

        public StageManager()
        {
            Broker.Global.Receive<RequestChangeStage>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Change(x.StagePrefab);
                })
                .AddTo(this.events);
        }

        ~StageManager()
        {
            this.events.Dispose();
        }

        private void Change(Stage nextStagePrefab)
        {
            if(this.current != null)
            {
                Object.Destroy(this.current.gameObject);
            }

            this.current = Object.Instantiate(nextStagePrefab);
        }
    }
}
