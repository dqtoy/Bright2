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
        private AIBundle aiBundle = default;

        private IReadOnlyList<ScriptableAIElement> currentElements = default;

        private Actor owner;

        private readonly Dictionary<string, IReadOnlyList<ScriptableAIElement>> cachedAIElements = new Dictionary<string, IReadOnlyList<ScriptableAIElement>>();

        void Start()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);

            this.ChangeAI(this.aiBundle.EntryPointName);
        }

        public void ChangeAI(string name)
        {
            if(this.currentElements != null)
            {
                foreach (var element in this.currentElements)
                {
                    element.Exit();
                }
            }

            this.currentElements = this.GetAIElements(name);
            foreach (var element in this.currentElements)
            {
                element.Enter(this.owner, this);
            }
        }

        private IReadOnlyList<ScriptableAIElement> GetAIElements(string name)
        {
            if(this.cachedAIElements.ContainsKey(name))
            {
                return this.cachedAIElements[name];
            }

            var aiElements = this.aiBundle.Get(name).AIElements;
            var instance = new List<ScriptableAIElement>(aiElements.Count);
            foreach (var aiElement in aiElements)
            {
                instance.Add(Object.Instantiate(aiElement));
            }
            this.cachedAIElements.Add(name, instance);

            return instance;
        }
    }
}
