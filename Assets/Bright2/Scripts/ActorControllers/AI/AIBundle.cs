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
        private List<ScriptableAIElement> anyAIElements = default;

        /// <summary>
        /// 全ての状況下で実行される<see cref="ScriptableAIElement"/>
        /// </summary>
        /// <remarks>
        /// 常に実行されるので<see cref="IAIElement.Exit(Actor, ActorAIController)"/>は死亡時のみ実行されるので注意が必要です
        /// </remarks>
        public IReadOnlyList<ScriptableAIElement> AnyAIElements => this.anyAIElements;

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
