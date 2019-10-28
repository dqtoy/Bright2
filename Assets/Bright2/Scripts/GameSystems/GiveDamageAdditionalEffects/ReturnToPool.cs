using HK.Bright2.GimmickControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects
{
    /// <summary>
    /// 攻撃が当たった際にギミックをプールする
    /// </summary>
    [CreateAssetMenu(fileName = "ReturnToPool", menuName = "Bright2/GiveDamageActorAdditionalEffect/ReturnToPool")]
    public sealed class ReturnToPool : GiveDamageActorAdditionalEffect
    {
        public override void Do(IGiveDamage giveDamage)
        {
            var gimmick = giveDamage.Root.GetComponent<IPoolableComponent>();
            Assert.IsNotNull(gimmick, $"{giveDamage.Root}に{typeof(IPoolableComponent)}がありませんでした");

            gimmick.Return();
        }
    }
}
