using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Inputs
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReaderSO")]
    public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
    {
        private Controls _controls;

        public Vector2 Movement { get; private set; }
        public Vector2 MousePosition { get; private set; }

        public event Action OnAttackEvent;
        public event Action OnInteractEvent;
        public event Action<Vector2> OnMovementEvent;
        public event Action<Vector2> OnMouseMoveEvent;

        private void OnEnable()
        {
            if(_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public void EnablePlayerInput()
        {
            _controls.Player.Enable();
        }

        public void DisablePlayerInput()
        {
            _controls.Player.Disable();
        }

        public void EnableAllInputs()
        {
            _controls.Enable();
        }

        public void DisableAllInputs()
        {
            _controls.Disable();
        }

        #region Event Functions
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackEvent?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
            OnMouseMoveEvent?.Invoke(MousePosition);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
            OnMovementEvent?.Invoke(Movement);
        }
        #endregion
    }
}
