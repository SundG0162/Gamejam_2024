using BSM.Core.Cameras;
using BSM.Entities;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace BSM.Players.DamagePlayer
{
    public class DamagePlayer : Player
    {
        private DamagePlayerWeapon _weapon;

        private Sequence _tagSequence;
        [Header("Tag Skill Setting")]
        [SerializeField]
        private float _swipeRadius = 2f;
        [SerializeField]
        private float _swipeForce = 5f;
        [SerializeField]
        private int _maxDetectEnemy = 30;
        [SerializeField]
        private LayerMask _whatIsTarget;
        private Collider2D[] _targets;

        [Space]
        [SerializeField]
        private ParticleSystem _shockwaveParticle;

        protected override void Awake()
        {
            base.Awake();
            _weapon = GetEntityComponent<DamagePlayerWeapon>();
            _targets = new Collider2D[_maxDetectEnemy];
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            InputReader.OnMouseClickEvent += HandleOnAttackEvent;
            InputReader.OnMouseUpEvent += HandleOnTryAttackEvent;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputReader.OnMouseClickEvent -= HandleOnAttackEvent;
            InputReader.OnMouseUpEvent -= HandleOnTryAttackEvent;
        }

        private void HandleOnAttackEvent()
        {
            if (_weapon.CanAttack())
            {
                _weapon.SetupWeapon();
            }
        }

        private void HandleOnTryAttackEvent()
        {
            if (_weapon.IsSetupEnd)
                _weapon.Attack();
            else
                _weapon.UnsetWeapon();
        }

        public override void Join()
        {
            base.Join();
            StartCoroutine(TagSkillCoroutine());
        }

        private IEnumerator TagSkillCoroutine()
        {
            InputReader.DisablePlayerInput();
            SwipeEnemies();
            yield return new WaitForSeconds(0.2f);
            _weapon.SetupWeapon(0.4f);
            yield return new WaitForSeconds(0.6f);
            _weapon.Attack();
            yield return new WaitForSeconds(0.15f);
            InputReader.EnablePlayerInput();
        }

        private void SwipeEnemies()
        {
            int count = Physics2D.OverlapCircle(transform.position, _swipeRadius, new ContactFilter2D { layerMask = _whatIsTarget, useLayerMask = true, useTriggers = true }, _targets);
            _shockwaveParticle.Play();
            for (int i = 0; i < count; i++)
            {
                if (_targets[i].TryGetComponent(out EntityMover mover))
                {
                    Vector2 direction = _targets[i].transform.position - transform.position;
                    direction.Normalize();
                    mover.Knockback(direction * _swipeForce, 1f);
                }
            }
        }
    }
}
