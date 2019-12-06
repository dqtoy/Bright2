using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 1個単位のAIのインターフェイス
    /// </summary>
    public interface IAIElement
    {
        void Enter(Actor actor);

        void Exit();
    }
}
