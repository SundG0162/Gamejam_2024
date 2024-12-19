using System;
using System.Collections.Generic;
using UnityEngine;

namespace BSM.Core.StatSystem
{
    public enum EStatValueType
    {
        Normal,
        Infinite
    }

    [CreateAssetMenu(menuName = "SO/StatSystem/StatElement")]
    public class StatElementSO : ScriptableObject, ICloneable
    {
        public delegate void OnValueChangeHandler(StatElementSO stat, float prevValue, float currentValue);

        public event OnValueChangeHandler OnValueChangeEvent;

        public EStatValueType statValueType;
        [Space]
        public string statName;
        public string description;
        public string displayName;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private float _baseValue, _minValue, _maxValue;

        private Dictionary<object, float> _modifierDict = new Dictionary<object, float>();

        [field: SerializeField]
        public bool IsPercent { get; private set; }

        private float _modifyValue = 0;
        public Sprite Icon => _icon;

        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public float MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }

        public float Value
        {
            get
            {
                if (statValueType == EStatValueType.Infinite)
                    return float.MaxValue;
                return Mathf.Clamp(_baseValue + _modifyValue, _minValue, _maxValue);
            }
        }
        public bool IsMax => Mathf.Approximately(Value, _maxValue);
        public bool IsMin => Mathf.Approximately(Value, _minValue);

        public float BaseValue
        {
            get => _baseValue;
            set
            {
                float prevValue = Value;
                _baseValue = Mathf.Clamp(value, _minValue, _maxValue);
                TryInvokeValueChangeEvent(prevValue, Value);
            }
        }

        public void AddModifier(object key, float value)
        {
            float prevValue = Value;
            _modifyValue += value;
            if (_modifierDict.ContainsKey(key))
            {
                _modifyValue -= _modifierDict[key];
                _modifierDict[key] = value;
            }
            else
                _modifierDict.Add(key, value);
            TryInvokeValueChangeEvent(prevValue, Value);
        }

        public void RemoveModifier(object key)
        {
            if (_modifierDict.TryGetValue(key, out float value))
            {
                float prevValue = Value;
                _modifyValue -= value;
                _modifierDict.Remove(key);

                TryInvokeValueChangeEvent(prevValue, Value);
            }
        }

        public void ClearModifier()
        {
            float prevValue = Value;
            _modifierDict.Clear();
            _modifyValue = 0;
            TryInvokeValueChangeEvent(prevValue, Value);
        }

        private void TryInvokeValueChangeEvent(float prevValue, float currentValue)
        {
            if (!Mathf.Approximately(currentValue, prevValue))
            {
                OnValueChangeEvent?.Invoke(this, prevValue, currentValue);
            }
        }

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}
