using BSM.Core.StatSystem;
using BSM.Inputs;
using BSM.Players;
using Crogen.CrogenPooling;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace BSM.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitializable
    {
        private Entity _entity;
        private EntityStat _entityStat;
        private EntityRenderer _entityRenderer;

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
            _entityRenderer = entity.GetEntityComponent<EntityRenderer>();
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

        public void Dash(Vector2 direction)
        {
            StartCoroutine(DashCoroutine(direction));
        }

        private IEnumerator DashCoroutine(Vector2 direction)
        {
            _moveSpeedElement.AddModifier(this, 20f);
            for (int i = 0; i < 5; i++)
            {
                GhostTrail trail = gameObject.Pop(PoolType.GhostTrail, transform.position, Quaternion.identity) as GhostTrail;
                trail.Initialize(_entity);
                yield return new WaitForSeconds(0.03f);
            }
            _moveSpeedElement.RemoveModifier(this);
        }

    }
}
