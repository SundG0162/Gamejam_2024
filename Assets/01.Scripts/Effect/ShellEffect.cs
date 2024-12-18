using DG.Tweening;
using Unity.AppUI.UI;
using UnityEngine;

namespace BSM.Effects
{
    public class ShellEffect : MonoBehaviour
    {
        [SerializeField]
        private float _gravityMutiplier = 3f;
        [SerializeField]
        private float _gravity;
        private Vector2 _velocity;
        private float _rotateVelocity;
        private bool _isGrounded = false;
        private float _startY;

        public void Initialize(Vector2 force)
        {
            _gravity = Physics2D.gravity.y;
            _velocity = force;
            force.Normalize();
            float crossValue = Vector3.Cross(force, Vector3.up).normalized.z;
            _rotateVelocity = Random.Range(50, 500) * crossValue;
            _startY = transform.position.y;
            _isGrounded = false;
        }

        private void Update()
        {
            if (_isGrounded)
                return;
            if (transform.position.y + 1f < _startY)
            {
                _isGrounded = true;
                transform.DOJump(transform.position + new Vector3(_velocity.x / 3, 0), 0.3f, 1, 0.3f).SetEase(Ease.Linear)
                    .OnUpdate(() => transform.eulerAngles += new Vector3(0, 0, _rotateVelocity * Time.deltaTime))
                    .OnComplete(() => transform.DOJump(transform.position + new Vector3(_velocity.x / 5, 0), 0.2f, 1, 0.3f).SetEase(Ease.Linear)
                    .OnUpdate(() => transform.eulerAngles += new Vector3(0, 0, _rotateVelocity * Time.deltaTime))
                    .OnComplete(() => transform.rotation = Quaternion.identity));
            }
            _velocity.y += _gravity * _gravityMutiplier * Time.deltaTime;
            transform.position += (Vector3)_velocity * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, 0, _rotateVelocity * Time.deltaTime);
        }
    }
}
