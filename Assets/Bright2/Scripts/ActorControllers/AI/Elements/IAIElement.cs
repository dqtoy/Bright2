namespace HK.Bright2.ActorControllers.AIControllers
{
    /// <summary>
    /// 1個単位のAIのインターフェイス
    /// </summary>
    public interface IAIElement
    {
        void Enter(Actor owner, AIObserver ownerAI);

        void Exit(Actor owner, AIObserver ownerAI);
    }
}
