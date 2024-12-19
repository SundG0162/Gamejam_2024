using System;
using BSM.Core.StatSystem;
using BSM.Enemies;
using BSM.Entities;
using UnityEngine;

namespace BSM
{
    public class AttackEffectTrigger : MonoBehaviour
    {
        [SerializeField] private DamageCast _cast;
        private EntityAnimationTrigger _trigger;
        private void Awake()
        {
            _trigger = GetComponentInParent<EntityAnimationTrigger>();
            _trigger.OnAnimationTriggerEvent += HandleAttackEvt;
        }

        private void HandleAttackEvt(EAnimationTrigger trigger)
        {
            if ((trigger & EAnimationTrigger.Attack) != 0)
            {
                _cast.CastDamage();
            }
        }
    }
}
