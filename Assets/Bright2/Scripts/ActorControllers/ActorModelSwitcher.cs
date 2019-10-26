using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>が管理するモデルを切り替えるクラス
    /// </summary>
    public sealed class ActorModelSwitcher : MonoBehaviour
    {
        [SerializeField]
        private Transform modelParent;

        private readonly Dictionary<int, Renderer> dictionary = new Dictionary<int, Renderer>();

        private int currentModelId = 0;

        void Awake()
        {
            for (var i = 0; i < this.modelParent.childCount; i++)
            {
                var child = this.modelParent.GetChild(i);
                var model = child.GetComponentInChildren<Renderer>();
                Assert.IsNotNull(model);
                this.dictionary.Add(Animator.StringToHash(child.name), model);
                model.enabled = false;
            }

            this.currentModelId = ActorModelNames.Idle0;
            this.Change(this.currentModelId);
        }

        public void Change(int nextModelId)
        {
            this.dictionary[this.currentModelId].enabled = false;
            this.currentModelId = nextModelId;
            this.dictionary[this.currentModelId].enabled = true;
        }
    }
}
