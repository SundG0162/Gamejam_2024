using BSM.Animators;
using BSM.Core.Cameras;
using BSM.Entities;
using BSM.Inputs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Players
{
    public abstract class Player : Entity
    {
        [field: SerializeField]
        public InputReaderSO InputReader { get; private set; }
        protected EntityMover _entityMover;
        protected EntityRenderer _entityRenderer;
        protected PlayerTag _playerTag;

        [SerializeField]
        private AnimatorParameterSO _idleParameter;

        public event Action OnJoinEvent;
        public event Action OnQuitEvent;

        public bool StopFlip { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();
            _entityMover = GetEntityComponent<EntityMover>();
            _entityRenderer = GetEntityComponent<EntityRenderer>();
        }

        public void Initialize(PlayerTag tag)
        {
            _playerTag = tag;
        }

        protected virtual void OnEnable()
        {
            InputReader.OnMovementEvent += HandleOnMovementEvent;
        }

        protected virtual void OnDisable()
        {
            InputReader.OnMovementEvent -= HandleOnMovementEvent;
        }

        protected virtual void Update()
        {
            FlipToMouseCursor();
        }

        private void FlipToMouseCursor()
        {
            if (StopFlip) return;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(InputReader.MousePosition);
            Vector3 direction = transform.position - mousePos;
            float crossValue = Vector3.Cross(direction.normalized, Vector3.up).z;
            _entityRenderer.Flip(new Vector2(-crossValue, 0).normalized.x);
        }

        protected virtual void HandleOnMovementEvent(Vector2 movement)
        {
            if(movement == Vector2.zero)
                _entityRenderer.SetParameter(_idleParameter, true);
            else
                _entityRenderer.SetParameter(_idleParameter, false);
            _entityMover.SetMovement(movement);
        }

        public virtual void Join()
        {
            _entityRenderer.Appear(0.15f);
            OnJoinEvent?.Invoke();
        }

        public virtual void Quit()
        {
            _entityRenderer.Disappear(0.15f);
            OnQuitEvent?.Invoke();
        }

    }
}
