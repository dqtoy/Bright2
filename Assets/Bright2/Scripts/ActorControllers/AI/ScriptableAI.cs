using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// <see cref="ScriptableObject"/>で作成可能なAI
    /// </summary>
    public abstract class ScriptableAI : ScriptableObject, IAI
    {
        protected readonly CompositeDisposable events = new CompositeDisposable();
        
        public abstract void Enter(Actor owner);

        public virtual void Exit()
        {
            this.events.Clear();
        }
    }
}
