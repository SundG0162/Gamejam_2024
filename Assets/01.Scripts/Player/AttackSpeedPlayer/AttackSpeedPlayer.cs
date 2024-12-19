using System;
using UnityEngine;

namespace BSM.Players.AttackSpeedPlayer
{
    public class AttackSpeedPlayer : Player
    {
        private AttackSpeedPlayerWeapon _weapon;

        private bool _isFiring = false;

        protected override void Awake()
        {
            base.Awake();
            _weapon = GetEntityComponent<AttackSpeedPlayerWeapon>();
            InputReader.OnAttackEvent += HandleOnAttackEvent;
            InputReader.OnMouseUpEvent += HandleOnCancelAttackEvent;
        }

        protected override void Update()
        {
            base.Update();
            if (_isFiring)
            {
                if (_weapon.CanAttack())
                    _weapon.Attack();
            }
        }

        private void HandleOnCancelAttackEvent()
        {
            _isFiring = false;
        }

        private void HandleOnAttackEvent()
        {
            _isFiring = true;
        }
    }
}
