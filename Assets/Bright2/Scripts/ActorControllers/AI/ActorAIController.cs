using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// AIを監視して適宜AIを切り替えるクラス
    /// </summary>
    public sealed class ActorAIController : MonoBehaviour
    {
        [SerializeField]
        private AIBundle aiBundle = default;

        /// <summary>
        /// 追いかけている<see cref="Actor"/>
        /// </summary>
        public Actor ChaseTarget { get; set; }

        private string currentAIName;

        private IReadOnlyList<ScriptableAIElement> anyAIElements = default;

        private IReadOnlyList<ScriptableAIElement> currentElements = default;

        private Actor owner;

        private readonly Dictionary<string, IReadOnlyList<ScriptableAIElement>> cachedAIElements = new Dictionary<string, IReadOnlyList<ScriptableAIElement>>();

        void Start()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);

            this.anyAIElements = this.CreateInstanceAIElements(this.aiBundle.AnyAIElements);
            foreach (var ai in this.anyAIElements)
            {
                ai.Enter(this.owner, this);
            }

            this.ChangeAI(this.aiBundle.EntryPointName);
        }

        void OnDestroy()
        {
            foreach (var ai in this.anyAIElements)
            {
                ai.Exit(this.owner, this);
            }
            this.ExitAI();
        }

        public void ChangeAI(string name)
        {
            this.ExitAI();

            this.currentAIName = name;

            this.currentElements = this.GetAIElements(name);
            foreach (var element in this.currentElements)
            {
                element.Enter(this.owner, this);
            }
        }

        private void ExitAI()
        {
            if (this.currentElements == null)
            {
                return;
            }

            foreach (var element in this.currentElements)
            {
                element.Exit(this.owner, this);
            }
        }

        private IReadOnlyList<ScriptableAIElement> GetAIElements(string name)
        {
            if(this.cachedAIElements.ContainsKey(name))
            {
                return this.cachedAIElements[name];
            }

            var aiElements = this.aiBundle.Get(name).AIElements;
            var instance = CreateInstanceAIElements(aiElements);
            this.cachedAIElements.Add(name, instance);

            return instance;
        }

        private List<ScriptableAIElement> CreateInstanceAIElements(IReadOnlyList<ScriptableAIElement> aiElements)
        {
            var result = new List<ScriptableAIElement>(aiElements.Count);
            foreach (var aiElement in aiElements)
            {
                result.Add(Object.Instantiate(aiElement));
            }

            return result;
        }
    }
}
