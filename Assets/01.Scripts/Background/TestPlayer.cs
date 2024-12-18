using BSM.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SSH
{
    public class TestPlayer : MonoBehaviour
    {
        [SerializeField]private InputReaderSO _inputReaderSO;
        [SerializeField] private float _moveSpeed = 20;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _inputReaderSO.OnMovementEvent += HandleOnMovementEvent;
        }

        private void HandleOnMovementEvent(Vector2 dir)
        {
            transform.Translate(dir * _moveSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            if(Keyboard.current.dKey.wasPressedThisFrame)
                transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
        
        }
    }
}
