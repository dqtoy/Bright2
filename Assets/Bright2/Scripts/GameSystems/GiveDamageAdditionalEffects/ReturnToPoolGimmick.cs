using HK.Bright2.GimmickControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects
{
    /// <summary>
    /// 攻撃が当たった際にギミックをプールする
    /// </summary>
    [CreateAssetMenu(fileName = "ReturnToPoolGimmick", menuName = "Bright2/GiveDamageActorAdditionalEffect/ReturnToPoolGimmick")]
    public sealed class ReturnToPoolGimmick : GiveDamageActorAdditionalEffect
    {
        public override void Do(IGiveDamage giveDamage)
        {
            var gimmick = giveDamage.Root.GetComponent<Gimmick>();
            Assert.IsNotNull(gimmick, $"{giveDamage.Root}に{typeof(Gimmick)}がありませんでした");

            gimmick.Return();
        }
    }
}
