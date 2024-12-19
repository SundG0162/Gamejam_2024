using System.Collections;
using BSM.Core.DamageCalculator;
using BSM.Entities;
using Crogen.CrogenPooling;
using UnityEngine;

namespace BSM.Projectile
{
    public class Bullet : MonoBehaviour, IPoolingObject
    {
        [SerializeField] private float _speed; // 총알 속도
        [SerializeField] private float _moveTime; // 총알이 움직이는 시간
        [SerializeField] private LayerMask _whatIsPlayer; // 플레이어 레이어
        public float Damage; // 총알 데미지
        public Entity Dealer;
        private Entity _target;

        public PoolType OriginPoolType { get; set; }
        private EntityStat _stat;
        GameObject IPoolingObject.gameObject { get; set; }

        public void OnPop()
        {
            StartCoroutine(PushWaitTimeCo(_moveTime));
        }

        public void OnPush()
        {
            // 필요한 초기화 작업을 여기에 추가
        }

        void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
        }

        IEnumerator PushWaitTimeCo(float t)
        {
            yield return new WaitForSeconds(t);
            this.Push(); // 총알을 풀로 되돌림
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // 충돌한 객체가 플레이어 레이어인지 확인
            if (((1 << other.gameObject.layer) & _whatIsPlayer) != 0)
            {
                // IDamageable 인터페이스 확인
                IDamageable damageable = other.GetComponent<IDamageable>();
                _target = other.GetComponent<Entity>();

                float calcDmg = DamageCalculator.GetCaculatedDamage(Dealer, _target);
                if (damageable != null)
                {
                    damageable.ApplyDamage(other.transform, Damage, false, 0); // 데미지 적용
                }
                else
                {
                    Debug.LogWarning($"Object {other.name} does not implement IDamageable.");
                    return;
                }

                // 총알을 풀로 되돌림
                this.Push();
            }
        }
    }
}
