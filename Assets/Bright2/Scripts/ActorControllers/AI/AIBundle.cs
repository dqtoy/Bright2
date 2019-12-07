using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// AIをまとめるクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/AI/Bundle")]
    public sealed class AIBundle : ScriptableObject
    {
        [SerializeField]
        private string entryPointName = default;
        public string EntryPointName => this.entryPointName;

        [SerializeField]
        private List<Element> elements = default;

        public Element Get(string name)
        {
            var result = this.elements.Find(e => e.Name == name);
            Assert.IsNotNull(result, $"{name}に紐づくAIがありませんでした");

            return result;
        }

        [Serializable]
        public sealed class Element
        {
            [SerializeField]
            private string name = default;
            public string Name => this.name;

            [SerializeField]
            private List<ScriptableAIElement> aiElements = default;
            public IReadOnlyList<ScriptableAIElement> AIElements => this.aiElements;
        }
    }
}
