namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 1個単位のAIのインターフェイス
    /// </summary>
    public interface IAIElement
    {
        void Enter(Actor owner, ActorAIController ownerAI);

        void Exit(Actor owner, ActorAIController ownerAI);
    }
}
