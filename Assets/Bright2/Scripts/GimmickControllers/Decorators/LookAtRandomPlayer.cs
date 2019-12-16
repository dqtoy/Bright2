using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using UnityEngine;

namespace HK.Bright2.GimmickControllers.Decorators
{
    /// <summary>
    /// ランダムに選択されたプレイヤーの方向へ注視する<see cref="IGimmickDecorator"/>
    /// </summary>
    public sealed class LookAtRandomPlayer : MonoBehaviour, IGimmickDecorator
    {
        void IGimmickDecorator.OnActivate(Gimmick owner, Actor gimmickOwner)
        {
            var target = GameSystem.Instance.ActorManager.GetRandomPlayer();
            this.transform.right = target.CachedTransform.position - this.transform.position;
        }
    }
}
