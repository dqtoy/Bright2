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

            var initialElement = this.aiBundle.Get(this.aiBundle.EntryPointName);

            this.currentElements = this.GetAIElements(this.aiBundle.EntryPointName);
            foreach (var ai in this.currentElements)
            {
                ai.Enter(this.owner);
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
