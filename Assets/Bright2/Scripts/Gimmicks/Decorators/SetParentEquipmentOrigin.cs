using System;
using HK.Bright2.ActorControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// 攻撃した<see cref="Actor"/>の攻撃座標の親に<see cref="Gimmick"/>をアタッチするクラス
    /// </summary>
    public sealed class SetParentEquipmentOrigin : MonoBehaviour, IGimmickDecorator
    {
        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            var parent = gimmickOwner.TransformHolder.GetEquipmentOrigin(gimmickOwner.StatusController.Direction);
            owner.transform.SetParent(parent);
            owner.transform.localPosition = Vector3.zero;
        }
    }
}
