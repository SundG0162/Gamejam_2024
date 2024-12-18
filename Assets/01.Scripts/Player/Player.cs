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

        protected override void Awake()
        {
            base.Awake();
            InputReader.OnMovementEvent += HandleOnMovementEvent;
            _entityMover = GetEntityComponent<EntityMover>();
        }

        protected virtual void HandleOnMovementEvent(Vector2 movement)
        {
            _entityMover.SetMovement(movement);
        }
    }
}
