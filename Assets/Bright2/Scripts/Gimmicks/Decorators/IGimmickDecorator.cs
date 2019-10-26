using HK.Bright2.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// <see cref="Gimmick"/>に対して様々な効果を付与するインターフェイス
    /// </summary>
    public interface IGimmickDecorator
    {
        /// <summary>
        /// <see cref="Gimmick"/>が起動した際の処理
        /// </summary>
        void OnActivate(Gimmick owner, Actor gimmickOwner);
    }
}
