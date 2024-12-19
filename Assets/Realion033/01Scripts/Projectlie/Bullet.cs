using Crogen.CrogenPooling;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Projectile
{
    public class Bullet : MonoBehaviour, IPoolingObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _moveTime;
        private float currentTime = 0;

        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }

        public void OnPop()
        {

        }

        public void OnPush()
        {

        }

        void Update()
        {
            currentTime += Time.deltaTime;

            transform.Translate(Vector2.up * _speed * Time.deltaTime);

            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                gameObject.Pop(PoolType.EnemyBullet, new Vector2(0, 0), Quaternion.identity);
            }
            // if (currentTime >= _moveTime)
            // {
            //     Destroy(gameObject);
            // }
        }
    }
}
