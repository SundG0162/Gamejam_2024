using BSM.Core.StatSystem;
using BSM.Entities;
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

        protected float _damage, _attackDelay;

        [SerializeField]
        private StatElementSO _damageElement, _attackDelayElement;

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


        public virtual bool CanAttack()
        {
            Debug.Log($"{_lastAttackTime + _attackDelay} : {Time.time}");
            return _lastAttackTime + _attackDelay <= Time.time;
        }

        public abstract void Attack();
    }
}
