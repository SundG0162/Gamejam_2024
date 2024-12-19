using BSM.Core.Cameras;
using BSM.Entities;
using BSM.UI;
using DG.Tweening;
using UnityEngine;

namespace BSM.Players.DamagePlayer
{
    public class DamagePlayerWeapon : PlayerWeapon
    {
        private Animator _animator;
        private Material _sampleMaterial;

        private readonly int _blinkValueIDID = Shader.PropertyToID("_BlinkValue");
        private readonly int _dissolveAmountID = Shader.PropertyToID("_DissolveAmount");
        private readonly int _dissolveColorID = Shader.PropertyToID("_DissolveColor");
        private readonly int _dissolveNoiseScaleID = Shader.PropertyToID("_DissolveNoiseScale");
        private readonly int _blinkTriggerHash = Animator.StringToHash("Blink");

        public bool IsSetupEnd { get; private set; } = false;
        private bool _isCanceling = false;
        private Sequence _setupSequence;
        private Tween _shakeTween;

        [Space]
        [Header("Damage Cast Setting")]
        [SerializeField]
        private float _radius;
        [SerializeField]
        private int _maxDetectEnemy;
        [SerializeField]
        private LayerMask _whatIsTarget;
        private Collider2D[] _targets;

        [Space]
        [Header("Material Setting")]
        [SerializeField]
        [ColorUsage(true, true)]
        private Color _dissolveColor;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _animator = _visualTrm.GetComponent<Animator>();
            _sampleMaterial = _visualTrm.GetComponent<SpriteRenderer>().material;
            _sampleMaterial.SetVector(Shader.PropertyToID("_SheetSize"), new Vector2(16, 48));
            _sampleMaterial.SetFloat(_dissolveAmountID, 0);
            _sampleMaterial.SetColor(_dissolveColorID, _dissolveColor);

            _targets = new Collider2D[_maxDetectEnemy];
        }

        public void SetupWeapon(float setupTime = 2f)
        {
            if (_setupSequence != null && _setupSequence.IsActive())
                _setupSequence.Kill();
            IsSetupEnd = false;
            _pivotTrm.localRotation = Quaternion.identity;
            _sampleMaterial.SetFloat(_blinkTriggerHash, 0);
            _sampleMaterial.SetFloat(_dissolveAmountID, 0);
            _sampleMaterial.SetFloat(_dissolveNoiseScaleID, Random.Range(4f, 16f));

            _setupSequence = DOTween.Sequence();
            _setupSequence
                .Append(DOTween.To(() => _sampleMaterial.GetFloat(_dissolveAmountID), v => _sampleMaterial.SetFloat(_dissolveAmountID, v), 0.7f, setupTime).SetEase(Ease.OutSine))
                .AppendCallback(() => _sampleMaterial.SetFloat(_blinkValueIDID, 1))
                .Append(DOTween.To(() => _sampleMaterial.GetFloat(_blinkValueIDID), v => _sampleMaterial.SetFloat(_blinkValueIDID, v), 0, 0.15f))
                .JoinCallback(() =>
                {
                    _animator.SetBool(_blinkTriggerHash, true);
                    IsSetupEnd = true;
                    _shakeTween = transform.DOShakePosition(0.5f, 0.05f, 50, 90, fadeOut: false).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
                });
        }

        public override bool CanAttack()
        {
            return base.CanAttack() && !_isCanceling;
        }

        public void UnsetWeapon()
        {
            if (_setupSequence != null && _setupSequence.IsActive())
                _setupSequence.Kill();
            _animator.SetBool(_blinkTriggerHash, false);
            _isCanceling = true;
            _sampleMaterial.SetFloat(_blinkTriggerHash, 0.2f);
            DOTween.To(() => _sampleMaterial.GetFloat(_dissolveAmountID), v => _sampleMaterial.SetFloat(_dissolveAmountID, v), 0, 0.15f)
                .OnComplete(() => _isCanceling = false);
            IsSetupEnd = false;
        }

        public override void Attack()
        {
            base.Attack();
            _isCanceling = true;
            _player.StopFlip = true;
            _shakeTween.Kill();
            CastDamage();
            _pivotTrm.DOLocalRotate(new Vector3(0, 0, -380f), 0.25f, RotateMode.FastBeyond360)
                .OnComplete(() =>
                {
                    UnsetWeapon();
                    _player.StopFlip = false;
                    _lastAttackTime = Time.time;
                });
            CameraManager.Instance.ShakeCamera(6, 3, 0.3f);
        }

        public void CastDamage()
        {
            int count = Physics2D.OverlapCircle(transform.position, _radius, new ContactFilter2D { useLayerMask = true, layerMask = _whatIsTarget, useTriggers = true }, _targets);
            if(count >= 10)
            {
                _player.GetEntityComponent<EntityHealth>().Heal(20);
            }
            for (int i = 0; i < count; i++)
            {
                if (_targets[i].TryGetComponent(out IDamageable target))
                {
                    target.ApplyDamage(_player.transform, _damage, false, 30);
                }
            }
        }
    }
}
