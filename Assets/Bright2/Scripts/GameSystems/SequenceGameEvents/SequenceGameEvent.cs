using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// 一連の流れを持つゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/SequenceGameEvent")]
    public sealed class SequenceGameEvent : ScriptableObject, ISequenceGameEvent
    {
        [SerializeField]
        private SequenceGameEventElement entryPointEvent = default;

        void IGameEvent.Invoke(Actor invokedActor)
        {
            this.entryPointEvent.Invoke(this);
        }

        void ISequenceGameEvent.Next(ISequenceGameEventElement nextEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
