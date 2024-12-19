using BSM.Core.StatSystem;
using DG.Tweening;
using System;
using UnityEngine;

namespace BSM.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable
    {
        private Entity _entity;
        private EntityMover _entityMover;
        private EntityStat _entityStat;
        private EntityRenderer _entityRenderer;

        public delegate void DamageTakenEvent(Transform dealer, float damage, bool isCritical);
        public delegate void HealthChangeEvent(float prevHealth, float currentHealth);

        public event DamageTakenEvent OnDamageTakenEvent;
        public event HealthChangeEvent OnHealthChangeEvent;
        public event Action OnDeadEvent;

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        [SerializeField]
        private StatElementSO _maxHealthElement;


        public void Initialize(Entity entity)
        {
            _entity = entity;
            _entityMover = entity.GetEntityComponent<EntityMover>();
            _entityStat = entity.GetEntityComponent<EntityStat>();
            _entityRenderer = entity.GetEntityComponent<EntityRenderer>();
            _maxHealthElement = _entityStat.GetStatElement(_maxHealthElement);
            MaxHealth = _maxHealthElement.Value;
            CurrentHealth = MaxHealth;
            _maxHealthElement.OnValueChangeEvent += HandleOnMaxHealthChangeEvent;
        }

        private void HandleOnMaxHealthChangeEvent(StatElementSO stat, float prevValue, float currentValue)
        {
            MaxHealth = currentValue;
            float prevHealth = CurrentHealth;
            if (prevValue < currentValue)
            {
                CurrentHealth += currentValue - prevValue;
            }
            OnHealthChangeEvent?.Invoke(prevHealth, CurrentHealth);
        }

        public void ApplyDamage(Transform dealer, float damage, bool isCritical, float knockbackPower, float knockbackTime = 0.3f)
        {
            _entityRenderer.Blink();
            Vector2 direction = transform.position - dealer.position;
            direction.Normalize();
            if (knockbackPower != 0)
                _entityMover.Knockback(direction * knockbackPower, knockbackTime);
            float prevHealth = CurrentHealth;
            OnDamageTakenEvent?.Invoke(dealer, damage, isCritical);
            CurrentHealth -= damage;
            OnHealthChangeEvent?.Invoke(prevHealth, CurrentHealth);
            if(CurrentHealth <= 0)
            {
                OnDeadEvent?.Invoke();
            }
        }
    }
}
