using UnityEngine;
using BSM.Core.StatSystem;
using BSM.Entities;

namespace BSM.Enemies
{
    public class DamageCast : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float _attackRange;
        [SerializeField] private StatElementSO _damage;

        private BTEnemy _enemy;
        private EntityStat _entitystat;
        private Entity _entity;

        public void CastDamage()
        {
            Transform target = _enemy.GetTargetInRadius(_attackRange);
            _enemy.Attack(_damage.Value, target);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _enemy = GetComponentInParent<BTEnemy>();
            _entitystat = _entity.GetEntityComponent<EntityStat>();
            _damage = _entitystat.GetStatElement(_damage);
        }
    }
}
