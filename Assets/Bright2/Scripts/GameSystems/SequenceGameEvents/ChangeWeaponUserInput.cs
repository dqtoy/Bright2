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
    /// 装備している武器を切り替えるシーケンスゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/SequenceGameEvent/Element/ChangeWeaponUserInput")]
    public sealed class ChangeWeaponUserInput : SequenceGameEventElement
    {
        [SerializeField]
        private SequenceGameEventElement nextElement = default;

        public override void Invoke(ISequenceGameEvent owner, Actor invoker)
        {
            Broker.Global.Publish(ChangeWeaponUserInputMessages.Request.Get(invoker));

            Broker.Global.Receive<ChangeWeaponUserInputMessages.End>()
                .Take(1)
                .SubscribeWithState3(this, owner, invoker, (_, _this, _owner, _invoker) =>
                {
                    _owner.Next(_this.nextElement, _invoker);
                })
                .AddTo(invoker);
        }
    }
}
