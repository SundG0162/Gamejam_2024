using BSM.Entities;
using Crogen.CrogenPooling;
using UnityEngine;
using Unity.Behavior;
using BSM.UI;
using System;

namespace BSM.Enemies
{
    public class BTEnemy : Entity, IPoolingObject
    {
        [SerializeField] protected LayerMask _whatIsTarget;
        protected EntityHealth _health;
        protected BoxCollider2D _coll;
        protected BehaviorGraphAgent _btAgent;
        protected override void Awake()
        {
            base.Awake();

            _coll = GetComponent<BoxCollider2D>();
            _btAgent = GetComponent<BehaviorGraphAgent>();
            GetEntityComponent<EntityHealth>().OnDamageTakenEvent += HandleOnDamageTaken;

            _health.OnDeadEvent += HandleDeadEvt;
        }

        public virtual void HandleDeadEvt()
        {
            _coll.enabled = false;
        }

        private void HandleOnDamageTaken(Transform dealer, float damage, bool isCritical)
        {
            //여기 물 enum 추가 안하면 에러남
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

        public PoolType OriginPoolType { get; set; }

        GameObject IPoolingObject.gameObject { get; set; }

        public void OnPop()
        {
        }

        public void OnPush()
        {
        }
    }
}
