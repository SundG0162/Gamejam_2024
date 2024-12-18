using System;
using BSM.Entities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BSM.Enemies
{
    public class MeleeEnemy : BTEnemy
    {
        private EntityHealth _health;
        private EntityRenderer _renderer;
        private MeleeEnemy _meleeEnemy;
        //private readonly int _dissolveAmountID = Shader.PropertyToID("_DissoleAmount");

        protected override void Awake()
        {
            base.Awake();

            _health = GetComponent<EntityHealth>();
            _renderer = GetComponentInChildren<EntityRenderer>();
            _meleeEnemy = this;
            _health.OnDeadEvent += HandleDeadEvt;
        }

        private void Update()
        {
            if (Keyboard.current.zKey.wasPressedThisFrame)
            {
                _health.ApplyDamage(_meleeEnemy, 1, false, 0);
            }
        }

        private void HandleDeadEvt()
        {
            Debug.Log("DIE");
            _renderer.Dissolve(0f, 2.5f);
        }
    }
}
