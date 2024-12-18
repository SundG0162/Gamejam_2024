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

        public bool StopFlip { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();
            InputReader.OnMovementEvent += HandleOnMovementEvent;
            _entityMover = GetEntityComponent<EntityMover>();
            _entityRenderer = GetEntityComponent<EntityRenderer>();
        }

        protected virtual void OnDisable()
        {
            InputReader.OnMovementEvent -= HandleOnMovementEvent;
        }

        private void Update()
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
            _entityMover.SetMovement(movement);
        }
    }
}
