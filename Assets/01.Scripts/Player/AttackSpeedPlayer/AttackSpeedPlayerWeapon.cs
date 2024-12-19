using BSM.Core.StatSystem;
using BSM.Entities;
using UnityEngine;

namespace BSM.Players.AttackSpeedPlayer
{
    public class AttackSpeedPlayerWeapon : PlayerWeapon
    {
        [Space]
        [Header("Stat Setting")]
        [SerializeField]
        private StatElementSO _overheatThresholdElement;

        [Space]
        [Header("Position Settting")]
        [SerializeField]
        private Transform _muzzleTrm;
        [SerializeField]
        private Transform _shellExitTrm;


        private float _currentOverheat;
        private float _overheatThreshold;

        private float _coolingSpeed = 30f;


        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _overheatThresholdElement = entity.GetEntityComponent<EntityStat>().GetStatElement(_overheatThresholdElement);
            _overheatThreshold = _overheatThresholdElement.Value;
            _overheatThresholdElement.OnValueChangeEvent += HandleOnOverheatThresholdChangeEvent;
        }

        private void Update()
        {
            _currentOverheat -= Time.deltaTime * _coolingSpeed;
            FacingMouseCursor();
        }

        private void FacingMouseCursor()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(_player.InputReader.MousePosition);
            Vector3 direction = mousePos - transform.position;
            direction.Normalize();
            float crossValue = Vector3.Cross(direction, Vector3.up).z;
            if (crossValue < 0)
                direction.x *= -1;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _pivotTrm.localRotation = Quaternion.Euler(0, 0, angle);
        }

        private void HandleOnOverheatThresholdChangeEvent(StatElementSO stat, float prevValue, float currentValue)
        {
            _overheatThreshold = currentValue;
            if (_currentOverheat > _overheatThreshold)
                _currentOverheat = _overheatThreshold;
        }

        public override bool CanAttack()
        {
            return base.CanAttack() && _currentOverheat < _overheatThreshold;
        }

        public override void Attack()
        {
            base.Attack();
            _currentOverheat += 10f;
        }
    }
}
