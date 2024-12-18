using UnityEngine;

namespace BSM.Entities
{
    public interface IDamageable
    {
        public void ApplyDamage(Entity dealer, float damage, bool isCritical, float knockbackPower, float knockbackTime = 0.3f);
    }
}
