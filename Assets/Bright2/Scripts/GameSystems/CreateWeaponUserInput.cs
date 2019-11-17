using HK.Bright2.ActorControllers;
using HK.Bright2.Extensions;
using HK.Bright2.GameSystems.Messages;
using HK.Bright2.UIControllers.Messages;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems
{
    /// <summary>
    /// ユーザーの入力によって<see cref="Actor"/>に対して武器を追加するクラス
    /// </summary>
    public sealed class CreateWeaponUserInput : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<RequestCreateWeaponUserInput>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.StartSequence(x.Actor);
                })
                .AddTo(this);
        }

        private void StartSequence(Actor actor)
        {
            var items = GameSystem.Instance.MasterData.WeaponRecipe.GetViewableRecipes(actor.StatusController.Inventory);
            Broker.Global.Publish(RequestShowGridUI.Get(items, i =>
            {

            }, () =>
            {

            }));
        }
    }
}
