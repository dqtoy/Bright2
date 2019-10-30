using System.Collections.Generic;
using HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.ActorControllers.AbnormalConditionControllers
{
    /// <summary>
    /// 状態異常を制御するクラス
    /// </summary>
    public sealed class AbnormalConditionController
    {
        public readonly List<IAbnormalCondition> elements = new List<IAbnormalCondition>();

        private readonly Actor owner;

        public AbnormalConditionController(Actor owner)
        {
            this.owner = owner;
        }

        public void Attach(Constants.AbnormalStatus type)
        {
            // すでに状態異常にかかっている場合はなにもしない
            if(this.elements.FindIndex(e => e.Type == type) != -1)
            {
                return;
            }

            var element = AbnormalConditionFactory.Create(type);
            this.elements.Add(element);
            element.Attach(this.owner)
                .SubscribeWithState2(this, element, (_, _this, _element) =>
                {
                    _this.elements.Remove(_element);
                    _element.Detach();
                })
                .AddTo(this.owner);
        }


    }
}
