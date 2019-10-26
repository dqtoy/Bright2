using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>のアニメーションを制御するクラス
    /// </summary>
    public sealed class ActorAnimationController
    {
        private IDisposable updater;

        private Actor owner;

        private float duration;

        private int index;

        private ActorAnimationSequence sequence;

        public ActorAnimationController(Actor owner)
        {
            this.owner = owner;
        }

        public void StartSequence(ActorAnimationSequence sequence)
        {
            if(this.updater != null)
            {
                this.updater.Dispose();
                this.updater = null;
            }

            this.sequence = sequence;
            this.duration = 0.0f;
            this.index = -1;
            this.NextAnimation();
            this.updater = this.owner.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.duration += Time.deltaTime;
                    var waitDuration = _this.sequence.Elements[_this.index].WaitSeconds;
                    if(waitDuration < _this.duration)
                    {
                        _this.NextAnimation();
                    }
                });
        }

        private void NextAnimation()
        {
            this.index++;
            if(this.sequence.Elements.Count <= this.index)
            {
                this.index = 0;
            }

            this.owner.ModelSwitcher.Change(this.sequence.Elements[this.index].AnimationName);
            this.duration = 0.0f;
        }
    }
}
