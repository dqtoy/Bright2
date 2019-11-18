using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems.Messages;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.SequenceGameEvents
{
    /// <summary>
    /// 武器にアイテム修飾を装着するシーケンスゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/Element/AttachItemModifierToWeaponUserInput")]
    public sealed class AttachItemModifierToWeaponUserInput : SequenceGameEventElement
    {
        [SerializeField]
        private SequenceGameEventElement nextElement = default;

        public override void Invoke(ISequenceGameEvent owner, Actor invoker)
        {
            Broker.Global.Publish(AttachItemModifierToWeaponUserInputMessages.Request.Get(invoker));

            Broker.Global.Receive<AttachItemModifierToWeaponUserInputMessages.End>()
                .Take(1)
                .SubscribeWithState3(this, owner, invoker, (_, _this, _owner, _invoker) =>
                {
                    _owner.Next(_this.nextElement, _invoker);
                })
                .AddTo(invoker);
        }
    }
}
