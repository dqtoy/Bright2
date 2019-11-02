using System;
using HK.Framework;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 1個単位のダメージUIを制御するクラス
    /// </summary>
    public sealed class DamageUIElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text = default;

        [SerializeField]
        private float visibleSeconds = default;

        [SerializeField]
        private float randomPosition = default;

        [SerializeField]
        private Color damageColor = default;

        [SerializeField]
        private Color recoveryColor = default;

        private readonly static ObjectPoolBundle<DamageUIElement> pools = new ObjectPoolBundle<DamageUIElement>();

        private ObjectPool<DamageUIElement> pool;

        /// <summary>
        /// ダメージが発生したワールド空間の座標
        /// </summary>
        private Vector2 generationSource;

        private Canvas canvas;

        private RectTransform canvasTransform;

        private Camera worldCamera;

        void Awake()
        {
            this.LateUpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.UpdatePosition();
                });
        }

        public DamageUIElement Rent()
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;

            Observable.Timer(TimeSpan.FromSeconds(clone.visibleSeconds))
                .SubscribeWithState(clone, (_, _clone) =>
                {
                    _clone.pool.Return(_clone);
                })
                .AddTo(clone);

            return clone;
        }

        public void Setup(int damage, Vector2 generationSource, Canvas canvas, RectTransform canvasTransform, Camera worldCamera)
        {
            this.text.text = Mathf.Abs(damage).ToString();
            this.text.color = damage >= 0 ? this.damageColor : this.recoveryColor;

            var randomPosition = new Vector2(UnityEngine.Random.Range(-this.randomPosition, this.randomPosition), UnityEngine.Random.Range(-this.randomPosition, this.randomPosition));
            this.generationSource = generationSource + randomPosition;
            this.canvas = canvas;
            this.canvasTransform = canvasTransform;
            this.worldCamera = worldCamera;

            this.UpdatePosition();
        }

        private void UpdatePosition()
        {
            var pos = Vector2.zero;
            var uiCamera = this.canvas.worldCamera;
            var screenPos = RectTransformUtility.WorldToScreenPoint(this.worldCamera, this.generationSource);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasTransform, screenPos, uiCamera, out pos);
            this.transform.localPosition = pos;
        }
    }
}
