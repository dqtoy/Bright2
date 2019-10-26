using System;
using System.Collections.Generic;
using HK.Bright2.ActorControllers.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>アニメーションのシーケンスを持つクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/ActorAnimationSequence")]
    public sealed class ActorAnimationSequence : ScriptableObject
    {
        [SerializeField]
        private List<Element> elements = default;
        public List<Element> Elements => this.elements;

        [Serializable]
        public class Element
        {
            [SerializeField]
            private string animationName = default;
            public int AnimationName
            {
                get
                {
                    Debug.Log(animationName);
                    if(this.cachedAnimationId == 0)
                    {
                        this.cachedAnimationId = Animator.StringToHash(this.animationName);
                        Debug.Log(this.cachedAnimationId);
                    }

                    return this.cachedAnimationId;
                }
            }

            private int cachedAnimationId = 0;


            [SerializeField]
            private float waitSeconds = default;
            public float WaitSeconds => this.waitSeconds;
        }
    }
}
