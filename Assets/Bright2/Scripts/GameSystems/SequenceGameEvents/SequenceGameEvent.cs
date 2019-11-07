using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
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
            Broker.Global.Publish(StartSequenceGameEvent.Get());
            this.entryPointEvent.Invoke(this);
        }

        void ISequenceGameEvent.Next(ISequenceGameEventElement nextEvent)
        {
            nextEvent.Invoke(this);
        }

        void ISequenceGameEvent.Complete()
        {
            Broker.Global.Publish(EndSequenceGameEvent.Get());
        }
    }
}
