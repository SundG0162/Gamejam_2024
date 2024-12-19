using Crogen.CrogenPooling;
using UnityEngine;

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
            throw new System.NotImplementedException();
        }

        public void OnPush()
        {
            throw new System.NotImplementedException();
        }

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
