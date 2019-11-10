using HK.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.UIControllers
{
    /// <summary>
    /// 選択肢UIの要素を制御するクラス
    /// </summary>
    public sealed class ChoicesUIElement : MonoBehaviour, IPoolableComponent
    {
        [SerializeField]
        private TextMeshProUGUI message = default;

        [SerializeField]
        private Color selectedColor = default;

        [SerializeField]
        private Color unselectedColor = default;

        private static readonly ObjectPoolBundle<ChoicesUIElement> pools = new ObjectPoolBundle<ChoicesUIElement>();

        private ObjectPool<ChoicesUIElement> pool;

        public ChoicesUIElement Rent()
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;

            return clone;
        }

        public void Return()
        {
            this.pool.Return(this);
        }

        public void Setup(string message)
        {
            this.message.text = message;
        }

        public void SetColor(bool selected)
        {
            this.message.color = selected ? this.selectedColor : this.unselectedColor;
        }
    }
}
