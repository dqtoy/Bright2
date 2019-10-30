using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Bright2.GameSystems.GiveDamageActorAdditionalEffects
{
    /// <summary>
    /// ダメージを与えた際に状態異常も付与するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Bright2/GiveDamageActorAdditionalEffect/AttachAbnormalCondition")]
    public sealed class AttachAbnormalCondition : GiveDamageActorAdditionalEffect
    {
        [SerializeField]
        private Constants.AbnormalStatus type = default;

        public override void Do(IGiveDamage giveDamage)
        {
            giveDamage.Damager.AbnormalConditionController.Attach(this.type);
        }
    }
}
