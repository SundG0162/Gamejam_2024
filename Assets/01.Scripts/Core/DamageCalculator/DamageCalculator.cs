using BSM.Core.StatSystem;
using BSM.Entities;
using UnityEngine;

namespace BSM.Core.DamageCalculator
{
    public static class DamageCalculator
    {
        private static StatElementSO _damageElement;
        private static StatElementSO _armorElement;
        private static StatElementSO _armorPenetrationElement;
        static DamageCalculator()
        {
            _damageElement = ScriptableObject.CreateInstance<StatElementSO>();
            _damageElement.statName = "Damage";
            _armorElement = ScriptableObject.CreateInstance<StatElementSO>();
            _armorElement.statName = "Armor";
            _armorPenetrationElement = ScriptableObject.CreateInstance<StatElementSO>();
            _armorPenetrationElement.statName = "ArmorPenetration";
        }

        public static float GetCaculatedDamage(Entity dealer, Entity target)
        {
            if (dealer == null || target == null)
                return 0;
            EntityStat dealerStat = dealer.GetEntityComponent<EntityStat>();
            EntityStat targetStat = target.GetEntityComponent<EntityStat>();
            StatElementSO damage = dealerStat.GetStatElement(_damageElement);
            if (damage == null)
                return 0;
            StatElementSO armor = targetStat.GetStatElement(_armorElement);
            if (armor == null)
            {
                return damage.Value;
            }
            if (armor.statValueType == EStatValueType.Infinite)
            {
                return 0;
            }
            float calcArmor = armor.Value - armor.Value * 0.01f * _armorPenetrationElement.Value;
            float calcDamage = damage.Value - damage.Value * 0.01f * calcArmor;
            return calcDamage;
        }
    }
}
