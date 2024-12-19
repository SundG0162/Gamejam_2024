using BSM.Entities;
using Crogen.CrogenPooling;
using UnityEngine;
using Unity.Behavior;
using System;
using Crogen.CrogenPooling;
using BSM.UI;

namespace BSM.Enemies
{
    public class BTEnemy : Entity, IPoolingObject
    {
        [SerializeField] protected LayerMask _whatIsTarget;
        protected BehaviorGraphAgent _btAgent;
        private IPoolingObject _poolingObjectImplementation;

        protected override void Awake()
        {
            base.Awake();
            _btAgent = GetComponent<BehaviorGraphAgent>();
            GetEntityComponent<EntityHealth>().OnDamageTakenEvent += HandleOnDamageTaken;
        }

        private void HandleOnDamageTaken(Transform dealer, float damage, bool isCritical)
        {
            gameObject.Pop(PoolType.DamageText, transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * 0.2f, Quaternion.identity).gameObject.GetComponent<DamageText>().Initialize(damage);
        }

        public BlackboardVariable<T> GetVariable<T>(string variableName)
        {
            if (_btAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
            {
                return variable;
            }
            return null;
        }

        public void SetVariable<T>(string variableName, T value)
        {
            BlackboardVariable<T> variable = GetVariable<T>(variableName);
            Debug.Assert(variable != null, $"Variable {variableName} not found");
            variable.Value = value;
        }

        public Transform GetTargetInRadius(float radius)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, _whatIsTarget);
            return collider != null ? collider.transform : null;
        }

        public PoolType OriginPoolType
        {
            get => _poolingObjectImplementation.OriginPoolType;
            set => _poolingObjectImplementation.OriginPoolType = value;
        }

        public GameObject gameObject
        {
            get => _poolingObjectImplementation.gameObject;
            set => _poolingObjectImplementation.gameObject = value;
        }

        public void OnPop()
        {
            _poolingObjectImplementation.OnPop();
        }

        public void OnPush()
        {
            _poolingObjectImplementation.OnPush();
        }
    }
}
