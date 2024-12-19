using BSM.Core.Cameras;
using BSM.Core.StatSystem;
using BSM.Effects;
using BSM.Entities;
using Crogen.CrogenPooling;
using DG.Tweening;
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

        [Space]
        [SerializeField]
        private float _bulletSpeed = 50f;


        private float _currentOverheat;
        private float _overheatThreshold;

        [SerializeField]
        private float _coolingSpeed = 100f;

        private Tween _shakeTween;


        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _overheatThresholdElement = entity.GetEntityComponent<EntityStat>().GetStatElement(_overheatThresholdElement);
            _overheatThreshold = _overheatThresholdElement.Value;
            _overheatThresholdElement.OnValueChangeEvent += HandleOnOverheatThresholdChangeEvent;
            _player.OnJoinEvent += HandleOnJoinEvent;
        }

        private void HandleOnJoinEvent()
        {
            _currentOverheat = 0;
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
            CameraManager.Instance.ShakeCamera(0.2f, 1f, 0.1f);
            if (_shakeTween != null && _shakeTween.IsActive())
                _shakeTween.Complete();
            _shakeTween = transform.DOShakePosition(0.1f, 0.05f, 30, 90);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(_player.InputReader.MousePosition);
            mousePos += (Vector3)Random.insideUnitCircle * 0.15f;
            Vector2 direction = mousePos - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            PlayerBullet bullet = gameObject.Pop(PoolType.PlayerBullet, _muzzleTrm.position, Quaternion.identity) as PlayerBullet;
            bullet.Initialize(direction * _bulletSpeed, _player);
            ShellEffect shell = gameObject.Pop(PoolType.Shell, _shellExitTrm.position, Quaternion.identity).gameObject.GetComponent<ShellEffect>();
            Vector3 shellDir = _pivotTrm.right;
            shellDir.x *= Random.Range(-1f, -2.5f);
            shellDir.y += Random.Range(1f, 4f);
            shell.Initialize(shellDir);
            _currentOverheat += 10f;
        }
    }
}
