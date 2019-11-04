using System;
using HK.Framework;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 通知UIの要素を制御するクラス
    /// </summary>
    public sealed class NotificationUIElement : MonoBehaviour, IPoolableComponent
    {
        [SerializeField]
        private Image image = default;

        [SerializeField]
        private TextMeshProUGUI message = default;

        [SerializeField]
        private float visibleSeconds = default;

        private float currentVisibleSeconds;

        public bool IsVisible { get; private set; }

        private ObjectPool<NotificationUIElement> pool;

        private static readonly ObjectPoolBundle<NotificationUIElement> pools = new ObjectPoolBundle<NotificationUIElement>();

        void Update()
        {
            if(!this.IsVisible)
            {
                return;
            }

            this.currentVisibleSeconds += Time.deltaTime;
            if(this.currentVisibleSeconds > this.visibleSeconds)
            {
                this.Return();
            }
        }

        public void Setup(Sprite sprite, string message)
        {
            this.currentVisibleSeconds = 0.0f;
            this.image.sprite = sprite;
            this.message.text = message;
            this.IsVisible = true;
            this.currentVisibleSeconds = 0.0f;
        }

        public NotificationUIElement Rent()
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;

            return clone;
        }

        public void Return()
        {
            this.pool.Return(this);
            this.IsVisible = false;
        }
    }
}
