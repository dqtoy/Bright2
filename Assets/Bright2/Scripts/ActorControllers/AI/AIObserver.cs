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
        private AIBundle aiBundle = default;

        private AIBundle.Element currentElement = default;

        private Actor owner;

        void Start()
        {
            this.owner = this.GetComponent<Actor>();
            Assert.IsNotNull(this.owner);

            var initialElement = this.aiBundle.Get(this.aiBundle.EntryPointName);

            this.currentElement = initialElement;
            foreach (var ai in this.currentElement.AIElements)
            {
                ai.Enter(this.owner);
            }
        }
    }
}
