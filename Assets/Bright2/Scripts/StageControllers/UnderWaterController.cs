using UnityEngine;
using UnityEngine.Assertions;
using HK.Bright2.ActorControllers;
using HK.Bright2.StageControllers.Messages;

namespace HK.Bright2.StageControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnderWaterController : MonoBehaviour, IActorReactionOnTriggerEnter2D, IActorReactionOnTriggerExit2D
    {
        void IActorReactionOnTriggerEnter2D.Do(Actor actor)
        {
            actor.Broker.Publish(EnterUnderWater.Get());
        }

        void IActorReactionOnTriggerExit2D.Do(Actor actor)
        {
            actor.Broker.Publish(ExitUnderWater.Get());
        }
    }
}
