using BSM.Entities;
using BSM.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Players.DamagePlayer
{
    public class DamagePlayer : Player
    {
        private DamagePlayerWeapon _weapon;

        protected override void Awake()
        {
            base.Awake();
            _weapon = GetEntityComponent<DamagePlayerWeapon>();

            InputReader.OnAttackEvent += HandleOnAttackEvent;
            InputReader.OnMouseUpEvent += HandleOnTryAttackEvent;
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
