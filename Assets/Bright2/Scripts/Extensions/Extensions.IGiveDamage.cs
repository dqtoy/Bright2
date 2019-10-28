using HK.Bright2.ActorControllers;
using HK.Bright2.GameSystems;

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

            target.StatusController.TakeDamage(self.DamagePower, generationSource);
            target.Movement.SetGravity(self.KnockbackDirection * self.KnockbackPower);
            target.StatusController.AddInfinityStatus(self.GiveDamageObject, self.InfinitySeconds);
            
            foreach(var a in self.AdditionalEffects)
            {
                a.Do(self);
            }
        }
    }
}
