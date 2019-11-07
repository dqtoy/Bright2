using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// シーケンスゲームイベントを完了するイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/Element/Complete")]
    public sealed class Complete : SequenceGameEventElement
    {
        public override void Invoke(ISequenceGameEvent owner)
        {
            owner.Complete();
        }
    }
}
