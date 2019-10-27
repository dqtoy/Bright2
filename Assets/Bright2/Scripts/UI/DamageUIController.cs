using HK.Bright2.ActorControllers;
using HK.Bright2.ActorControllers.Messages;
using HK.Bright2.GameSystems;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// ダメージUIを制御するクラス
    /// </summary>
    public sealed class DamageUIController : MonoBehaviour
    {
        [SerializeField]
        private DamageUIElement elementPrefab = default;

        [SerializeField]
        private Canvas canvas = default;

        private RectTransform canvasTransform;

        void Awake()
        {
            Assert.IsNotNull(this.canvas);

            this.canvasTransform = this.canvas.transform as RectTransform;

            Broker.Global.Receive<SpawnedActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveActor(x.Actor);
                })
                .AddTo(this);
        }

        private void ObserveActor(Actor actor)
        {
            actor.Broker.Receive<TakedDamage>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateElement(x.Damage, x.GenerationSource);
                })
                .AddTo(this);
        }

        private void CreateElement(int damage, Vector2 position)
        {
            var element = this.elementPrefab.Rent();
            element.transform.SetParent(this.transform, false);
            element.Setup(damage, position, this.canvas, this.canvasTransform, GameSystem.Instance.Cameraman.Camera);
        }
    }
}
