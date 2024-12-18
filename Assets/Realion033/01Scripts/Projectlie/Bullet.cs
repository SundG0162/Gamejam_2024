using UnityEngine;

namespace BSM.Projectile
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _moveTime;
        private float currentTime = 0;

        void Update()
        {
            currentTime += Time.deltaTime;

            transform.Translate(Vector2.up * _speed * Time.deltaTime);

            if (currentTime >= _moveTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
