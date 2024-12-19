using BSM.Entities;
using Crogen.CrogenPooling;
using UnityEngine;
using Unity.Behavior;
using BSM.UI;
using System;
using BSM.Players;

namespace BSM.Enemies
{
    public class BTEnemy : Entity, IPoolingObject
    {
        [SerializeField] private LayerMask _whatIsTarget;
        public bool isDashAttack = false;
        public GameObject _hpBar;
        protected EntityHealth _health;
        protected BoxCollider2D _coll;
        protected PlayerTag _tag;
        protected BehaviorGraphAgent _btAgent;
        protected override void Awake()
        {
            base.Awake();

            _tag = GameObject.Find("Player").GetComponent<PlayerTag>();
            _coll = GetComponent<BoxCollider2D>();
            _btAgent = GetComponent<BehaviorGraphAgent>();
            GetEntityComponent<EntityHealth>().OnDamageTakenEvent += HandleOnDamageTaken;

            GetEntityComponent<EntityHealth>().OnDeadEvent += HandleDeadEvt;

            _tag.OnPlayerChangeEvent += HandleChangeValue;
        }

        private void HandleChangeValue(Player player)
        {
            SetVariable("PlayerTrm", player.GetComponentInChildren<Player>().transform);
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

        public void Attack(float damage, Transform target)
        {
            if (target == null)
            {
                //Debug.Log("NO");
                return;
            }
            IDamageable damageable = target.GetComponent<IDamageable>();

            //Debug.Log(target);

            if (target != null)
            {
                //Debug.Log("Yes");
                damageable.ApplyDamage(transform, damage, false, 0);
            }
        }

        public PoolType OriginPoolType { get; set; }

        GameObject IPoolingObject.gameObject { get; set; }

        public void OnPop()
        {
        }

        public void OnPush()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDashAttack)
            {
                if (((1 << other.gameObject.layer) & _whatIsTarget) != 0)
                {
                    DashAttack(other);
                }
            }
        }

        public virtual void DashAttack(Collider2D other) { }
    }
}
