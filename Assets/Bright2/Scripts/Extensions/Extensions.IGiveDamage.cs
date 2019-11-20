using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;
using UnityEngine;

namespace HK.Bright2.Extensions
{
    /// <summary>
    /// <see cref="IGiveDamage"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// ダメージを与える
        /// </summary>
        public static void GiveDamage(this IGiveDamage self, Actor target)
        {
            // 貫通できる回数が0の場合はダメージを与えられない
            if(self.CurrentPenetrationCount == 0)
            {
                return;
            }

            if(self.Owner == target)
            {
                return;
            }

            if(!self.IncludeTags.Contains(target.tag))
            {
                return;
            }

            if(target.StatusController.IsInfinity(self.GiveDamageObject))
            {
                return;
            }

            var generationSource = self.GiveDamageCollider.ClosestPoint(target.CachedTransform.position);
            var damageResult = Calculator.GetDamageOnActor(self, target, generationSource);

            target.StatusController.TakeDamage(damageResult);
            target.Movement.SetGravity(self.KnockbackDirection * self.KnockbackPower);
            target.StatusController.AddInfinityStatus(self.GiveDamageObject, self.InfinitySeconds);

            var effect = self.EffectPrefab.Rent();
            effect.transform.position = generationSource;

            var selfPosition = self.transform.position;
            var targetPosition = target.CachedTransform.position;
            var direction = (new Vector2(targetPosition.x, targetPosition.y) - new Vector2(selfPosition.x, selfPosition.y)).GetHorizontalDirection();
            foreach(var i in effect.GetComponentsInChildren<ISyncDirection>())
            {
                i.Sync(direction);
            }

            self.CurrentPenetrationCount--;

            foreach(var a in self.AdditionalEffects)
            {
                a.Do(self);
            }
        }
    }
}
