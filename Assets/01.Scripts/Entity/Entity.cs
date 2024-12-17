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
                return (T)component; // as랑 차이점은 안정성. 만약 T가 존재하지 않을때 null을 반환하는 대신 InvalidException을 발생시킴.
            }

            Type findType = _componentDict.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_componentDict[findType];

            return default;
        }
    }
}
