using BSM.Core.Cameras;
using BSM.Inputs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Entities
{
    public class Player : Entity
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

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                GetEntityComponent<EntityHealth>().ApplyDamage(this, 5, false, 0);
                CameraManager.Instance.ShakeCamera(2, 1, 0.15f);
            }
        }
    }
}
