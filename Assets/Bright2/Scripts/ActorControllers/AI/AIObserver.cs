using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// AIを監視して適宜AIを切り替えるクラス
    /// </summary>
    public sealed class AIObserver : MonoBehaviour
    {
        [SerializeField]
        private List<Element> elements = default;

        [SerializeField]
        private string initialAIName = default;

        private Element currentElement = default;

        private Actor owner;

        void Start()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);

            var initialElement = this.elements.Find(m => m.Name == this.initialAIName);
            Assert.IsNotNull(initialElement, $"{this.initialAIName} に紐づくAIがありませんでした");

            this.currentElement = initialElement;
            this.currentElement.AI.Enter(this.owner);
        }

        [Serializable]
        public class Element
        {
            [SerializeField]
            private string name = default;
            public string Name => this.name;

            [SerializeField]
            private ScriptableAI ai = default;
            public ScriptableAI AI
            {
                get
                {
                    this.aiInstance = this.aiInstance ?? UnityEngine.Object.Instantiate(this.ai);

                    return this.aiInstance;
                }
            }

            private ScriptableAI aiInstance;
        }
    }
}
