using BSM.Effects;
using BSM.Entities;
using BSM.UI;
using Crogen.CrogenPooling;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace BSM.Players.DamagePlayer
{
    public class DamagePlayer : Player
    {
        private DamagePlayerWeapon _weapon;

        [SerializeField]
        private ShellEffect _shell;

        protected override void Awake()
        {
            base.Awake();
            _weapon = GetEntityComponent<DamagePlayerWeapon>();

            InputReader.OnAttackEvent += HandleOnAttackEvent;
            InputReader.OnMouseUpEvent += HandleOnTryAttackEvent;

            _entityRenderer.Disappear(0.15f);

        }

        protected override void Update()
        {
            base.Update();
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                ShellEffect shell = gameObject.Pop(PoolType.Shell, transform.position, Quaternion.identity).gameObject.GetComponent<ShellEffect>();
                Vector2 force = Random.insideUnitCircle;
                force.Normalize();
                if (force.y < 0)
                    force.y *= -1;
                if(force.x > 0)
                    force.x *= -1;
                Debug.Log(force);
                force *= 5;
                shell.Initialize(force);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputReader.OnAttackEvent -= HandleOnAttackEvent;
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
    }
}
