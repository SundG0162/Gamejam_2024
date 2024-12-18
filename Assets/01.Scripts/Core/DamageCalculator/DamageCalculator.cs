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
            EntityStat dealerStat = dealer.GetEntityComponent<EntityStat>();
            EntityStat targetStat = dealer.GetEntityComponent<EntityStat>();
            _armorPenetrationElement = dealerStat.GetStatElement(_armorPenetrationElement);
            if (_armorPenetrationElement == null)
                _armorPenetrationElement.BaseValue = 0;
            _damageElement = dealerStat.GetStatElement(_damageElement);
            _armorElement = targetStat.GetStatElement(_armorElement);
            float calcArmor = _armorElement.Value - _armorElement.Value * 0.01f * _armorPenetrationElement.Value;
            float calcDamage = _damageElement.Value - _damageElement.Value * 0.01f * calcArmor;
            return calcDamage;
        }
    }
}
