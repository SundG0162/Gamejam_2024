using UnityEngine;

namespace BSM.Core.StatSystem
{
    [System.Serializable]
    public class StatOverrider
    {
        [SerializeField]
        private StatElementSO _stat;
        [SerializeField]
        private EStatValueType _statValueType;
        [SerializeField]
        private bool _isUseOverride;
        [SerializeField]
        private float _overridedBaseValue;

        public StatOverrider(StatElementSO stat)
        {
            _stat = stat;
        }

        public StatElementSO CreateStat()
        {
            StatElementSO newStat = _stat.Clone() as StatElementSO;
            newStat.statValueType = _statValueType;
            if (_isUseOverride)
            {
                newStat.BaseValue = _overridedBaseValue;
            }
            return newStat;
        }
    }
}
