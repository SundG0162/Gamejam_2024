using BSM.Core.StatSystem;
using System.Linq;
using UnityEngine;

namespace BSM.Entities
{
    public class EntityStat : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;

        [SerializeField]
        private StatOverrider[] _statOverriders;
        private StatElementSO[] _statElements;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statElements = _statOverriders.Select(x => x.CreateStat()).ToArray();
        }

        public StatElementSO GetStatElement(StatElementSO statElement)
        {
            return _statElements.FirstOrDefault(x => x.statName == statElement.statName);
        }

        public bool TryGetStatElement(StatElementSO stat, out StatElementSO result)
        {
            result = _statElements.FirstOrDefault(x => x.statName == stat.statName);
            return result != null;
        }

        public void SetBaseValue(StatElementSO stat, float value)
           => GetStatElement(stat).BaseValue = value;

        public float GetBaseValue(StatElementSO stat)
            => GetStatElement(stat).BaseValue;

        public float IncreaseBaseValue(StatElementSO stat, float value)
            => GetStatElement(stat).BaseValue += value;

        public void AddModifier(StatElementSO stat, object key, float value)
            => GetStatElement(stat).AddModifier(key, value);

        public void RemoveModifier(StatElementSO stat, object key)
            => GetStatElement(stat).RemoveModifier(key);

        public void ClearAllModifier()
        {
            foreach (StatElementSO stat in _statElements)
            {
                stat.ClearModifier();
            }
        }
    }
}
