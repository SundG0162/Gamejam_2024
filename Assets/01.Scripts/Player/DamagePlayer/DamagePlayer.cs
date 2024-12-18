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

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                GetEntityComponent<EntityHealth>().ApplyDamage(this, 1, false, 0);
            }
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
