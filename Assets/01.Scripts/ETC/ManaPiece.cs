using BSM.Players;
using Crogen.CrogenPooling;
using UnityEngine;

namespace BSM.ETC
{
    public class ManaPiece : MonoBehaviour, IPoolingObject
    {
        [SerializeField]
        private LayerMask _whatIsPlayer;
        [SerializeField]
        private float _detectRadius = 2f;
        [SerializeField]
        private float _mana = 3f;
        private Transform _target;

        private float _speed = 0f;

        public PoolType OriginPoolType { get; set; }
        GameObject IPoolingObject.gameObject { get; set ; }

        public void OnPop()
        {
        }

        public void OnPush()
        {
            _speed = 0f;
            _target = null;
        }

        private void Update()
        {
            if(_target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                _speed += Time.deltaTime * 6f;
                return;
            }
            Collider2D target = Physics2D.OverlapCircle(transform.position, _detectRadius, _whatIsPlayer);
            if (target != null)
            {
                _target = target.transform;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                player.PlayerTag.ModifyMana(_mana);
                this.Push();
            }
        }
    }
}
