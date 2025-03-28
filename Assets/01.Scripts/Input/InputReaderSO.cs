using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BSM.Inputs
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReaderSO")]
    public class InputReaderSO : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
    {
        private Controls _controls;

        public Vector2 Movement { get; private set; }
        public Vector2 MousePosition { get; private set; }

        public event Action OnMouseClickEvent;
        public event Action OnMouseUpEvent;
        public event Action OnInteractEvent;
        public event Action OnDashEvent;
        public event Action<Vector2> OnMovementEvent;
        public event Action<Vector2> OnMouseMoveEvent;
        public event Action OnOpenStatUIEvent;
        public event Action<int> OnTagEvent;
        public event Action OnPauseEvent;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
                _controls.UI.SetCallbacks(this);
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
                OnMouseClickEvent?.Invoke();
            else if (context.canceled)
                OnMouseUpEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractEvent?.Invoke();
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

        public void OnOpenStatUI(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnOpenStatUIEvent?.Invoke();
        }

        public void OnTag1(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnTagEvent?.Invoke(1);
        }

        public void OnTag2(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnTagEvent?.Invoke(2);
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDashEvent?.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnPauseEvent?.Invoke();

        }
        #endregion

        #region Unused Functions
        public void OnNavigate(InputAction.CallbackContext context)
        {
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
        }

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
        }
        #endregion
    }
}
