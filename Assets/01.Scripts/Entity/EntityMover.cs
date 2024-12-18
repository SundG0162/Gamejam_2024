using BSM.Core.StatSystem;
using DG.Tweening;
using System;
using UnityEngine;

namespace BSM.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitializable
    {
        private Entity _entity;
        private EntityStat _entityStat;

        public event Action<Vector2> OnMovementEvent;

        [SerializeField]
        private StatElementSO _moveSpeedElement;

        private Rigidbody2D _rigidbody;

        public bool CanManualMove { get; set; } = true;
        private Vector2 _movement;
        private float _moveSpeed;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rigidbody = entity.GetComponent<Rigidbody2D>();
            _entityStat = entity.GetEntityComponent<EntityStat>();
        }

        public void OnAfterInitialize()
        {
            _moveSpeedElement = _entityStat.GetStatElement(_moveSpeedElement);
            _moveSpeed = _moveSpeedElement.Value;
            _moveSpeedElement.OnValueChangeEvent += HandleOnMoveSpeedChangeEvent;
        }

        private void OnDestroy()
        {
            _moveSpeedElement.OnValueChangeEvent -= HandleOnMoveSpeedChangeEvent;
        }

        private void FixedUpdate()
        {
            if (CanManualMove)
                _rigidbody.linearVelocity = _movement * _moveSpeed;
            OnMovementEvent?.Invoke(_movement);
        }

        private void HandleOnMoveSpeedChangeEvent(StatElementSO stat, float prevValue, float currentValue)
        {
            _moveSpeed = currentValue;
        }

        public void SetMovement(Vector2 movement)
        {
            _movement = movement;
        }

        public void StopImmediately()
        {
            _movement = Vector2.zero;
        }

        public void AddForce(Vector2 force, ForceMode2D forceMode)
            => _rigidbody.AddForce(force, forceMode);

        public void Knockback(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately();
            AddForce(force, ForceMode2D.Impulse);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }
    }
}
