using BSM.Core.DamageCalculator;
using BSM.Enemies;
using BSM.Entities;
using Crogen.CrogenPooling;
using System.Collections;
using UnityEngine;

namespace BSM.Players.AttackSpeedPlayer
{
    public class PlayerBullet : MonoBehaviour, IPoolingObject
    {
        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set; }

        private Rigidbody2D _rigidbody;

        private Entity _shooter;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector2 force, Entity shooter)
        {
            _shooter = shooter;
            _rigidbody.linearVelocity = force;
            force.Normalize();
            float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(3f);
            this.Push();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BTEnemy enemy))
            {
                float calcDamage = DamageCalculator.GetCaculatedDamage(_shooter, enemy);
                enemy.GetEntityComponent<EntityHealth>().ApplyDamage(transform, calcDamage, false, 3);
                this.Push();
            }
        }

        public void OnPop()
        {
        }

        public void OnPush()
        {
        }
    }
}
