using BSM.Core.StatSystem;
using BSM.Entities;
using BSM.UI;
using System;
using UnityEngine;

namespace BSM.Players
{
    public abstract class PlayerWeapon : MonoBehaviour, IEntityComponent
    {
        protected Player _player;
        protected EntityStat _entityStat;

        protected Transform _pivotTrm;
        protected Transform _visualTrm;

        protected float _lastAttackTime;

        [SerializeField]
        private StatElementSO _damageElement, _attackDelayElement;
        protected float _damage, _attackDelay;

        [SerializeField]
        private Bar _cooldownBar;

        public event Action OnAttackEvent;

        public virtual void Initialize(Entity entity)
        {
            _player = entity as Player;

            _entityStat = entity.GetEntityComponent<EntityStat>();

            _damageElement = _entityStat.GetStatElement(_damageElement);
            _damage = _damageElement.Value;
            _damageElement.OnValueChangeEvent += HandleOnDamageChangeEvent;

            _attackDelayElement = _entityStat.GetStatElement(_attackDelayElement);
            _attackDelay = _attackDelayElement.Value;
            _lastAttackTime = -_attackDelay;
            _attackDelayElement.OnValueChangeEvent += HandleOnAttackDelayChangeEvent;

            _pivotTrm = transform.Find("Pivot");
            _visualTrm = _pivotTrm.Find("Visual");
        }

        private void HandleOnDamageChangeEvent(StatElementSO stat, float prevValue, float currentValue)
            => _damage = currentValue;

        private void HandleOnAttackDelayChangeEvent(StatElementSO stat, float prevValue, float currentValue)
            => _attackDelay = currentValue;

        private void Update()
        {
            //���� ��ٿ� ǥ�����ִ� ��. �ڵ尡 ���̰� ���� ���� ���⵵ ������ �ϴ� �ּ�ó��.
            //float ratio = (Time.time - _lastAttackTime) / _attackDelay;
            //if (ratio <= 1)
            //{
            //    _cooldownBar.gameObject.SetActive(true);
            //    _cooldownBar.SetFillAmount(ratio);
            //}
            //else
            //{
            //    _cooldownBar.gameObject.SetActive(false);
            //}
        }

        public virtual bool CanAttack()
        {
            return _lastAttackTime + _attackDelay <= Time.time;
        }

        public virtual void Attack()
        {
            OnAttackEvent?.Invoke();
        }
    }
}
