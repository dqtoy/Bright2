using System.Collections.Generic;
using HK.Bright2.ActorControllers.AbnormalConditionControllers.Elements;
using HK.Bright2.ActorControllers.Messages;
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
            this.owner.Broker.Publish(AttachedAbnormalCondition.Get(type));

            // デタッチ通知が来たらデタッチする
            element.Attach(this.owner)
                .SubscribeWithState3(this, element, type, (_, _this, _element, _type) =>
                {
                    _this.elements.Remove(_element);
                    _element.Detach();

                    _this.owner.Broker.Publish(DetachedAbnormalCondition.Get(_type));
                })
                .AddTo(this.owner);
        }


    }
}
