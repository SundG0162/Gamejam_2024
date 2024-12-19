using BSM.Entities;
using Crogen.CrogenPooling;
using UnityEngine;
using Unity.Behavior;
using BSM.UI;
using System;
using BSM.Players;
using Unity.Mathematics;
using BSM.Core.DamageCalculator;
using BSM.Core.Audios;

namespace BSM.Enemies
{
    public class BTEnemy : Entity, IPoolingObject
    {
        [SerializeField] private LayerMask _whatIsTarget;
        public bool isDashAttack = false;
        public GameObject _hpBar;
        protected EntityHealth _health;
        protected Animator _animator;
        protected BoxCollider2D _coll;
        protected PlayerTag _tag;
        protected BehaviorGraphAgent _btAgent;
        protected override void Awake()
        {
            base.Awake();

            _tag = GameObject.Find("Player").GetComponent<PlayerTag>();
            _coll = GetComponent<BoxCollider2D>();
            _btAgent = GetComponent<BehaviorGraphAgent>();
            _animator = GetComponentInChildren<Animator>();
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
            _animator.enabled = false;
            _coll.enabled = false;

            int caughtEnemyCount = PlayerPrefs.GetInt("CaughtEnemy", 0);
            PlayerPrefs.SetInt("CaughtEnemy", caughtEnemyCount + 1);    

            // 30% 확률로 Pop 호출
            if (UnityEngine.Random.value <= 0.3f) // Random.value는 0.0f에서 1.0f 사이의 값을 반환
            {
                gameObject.Pop(PoolType.ManaPiece, transform.position, Quaternion.identity);
            }
        }

        private void HandleOnDamageTaken(Transform dealer, float damage, bool isCritical)
        {
            //여기 물 enum 추가 안하면 에러남
            AudioManager.Instance.PlayAudio("EnemyHit");
            gameObject.Pop(PoolType.DamageText, transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * 0.5f, Quaternion.identity).gameObject.GetComponent<DamageText>().Initialize(damage);
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
            float calcDmg = DamageCalculator.GetCaculatedDamage(this, target.GetComponent<Entity>());

            if (target != null)
            {
                damageable.ApplyDamage(transform, calcDmg, false, 0);
                AudioManager.Instance.PlayAudio("EnemySlash");
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
