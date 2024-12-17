using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BSM.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        private Dictionary<Type, IEntityComponent> _componentDict;

        protected virtual void Awake()
        {
            AddEntityComponentsToDictionary();
            InitializeComponent();
            AfterInitialize();
        }

        private void AddEntityComponentsToDictionary()
        {
            GetComponentsInChildren<IEntityComponent>().
                ToList().
                ForEach(component => _componentDict.Add(component.GetType(), component));
        }

        private void InitializeComponent()
        {
            _componentDict.Values.ToList().ForEach(component => component.Initialize(this));
        }

        private void AfterInitialize()
        {
            _componentDict.Values.OfType<IAfterInitializable>().ToList().ForEach(afterInitalizable => afterInitalizable.OnAfterInitialize());
        }

        public T GetEntityComponent<T>() where T : IEntityComponent
        {
            if (_componentDict.TryGetValue(typeof(T), out var component))
            {
                return (T)component; // as�� �������� ������. ���� T�� �������� ������ null�� ��ȯ�ϴ� ��� InvalidException�� �߻���Ŵ.
            }

            Type findType = _componentDict.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_componentDict[findType];

            return default;
        }
    }
}
