using BSM.Core.Cameras;
using BSM.Entities;
using DG.Tweening;
using UnityEngine;

namespace BSM.Players.DamagePlayer
{
    public class DamagePlayerWeapon : PlayerWeapon
    {
        private Animator _animator;
        private Material _sampleMaterial;

        private readonly int _blinkValue = Shader.PropertyToID("_BlinkValue");
        private readonly int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");
        private readonly int _blinkTriggerHash = Animator.StringToHash("Blink");

        public bool IsSetupEnd { get; private set; } = false;
        [SerializeField]
        private bool _isCanceling = false;
        private Sequence _setupSequence;
        private Tween _unsetTween;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _animator = _visualTrm.GetComponent<Animator>();
            _sampleMaterial = _visualTrm.GetComponent<SpriteRenderer>().material;
            _sampleMaterial.SetVector(Shader.PropertyToID("_SheetSize"), new Vector2(16, 48));
            _sampleMaterial.SetFloat(_dissolveAmount, 0);
        }

        public void SetupWeapon()
        {
            if (_setupSequence != null && _setupSequence.IsActive())
                _setupSequence.Kill();
            IsSetupEnd = false;
            _pivotTrm.rotation = Quaternion.identity;
            _sampleMaterial.SetFloat(_blinkTriggerHash, 0);
            _sampleMaterial.SetFloat(_dissolveAmount, 0);

            _setupSequence = DOTween.Sequence();
            _setupSequence
                .Append(DOTween.To(() => _sampleMaterial.GetFloat(_dissolveAmount), v => _sampleMaterial.SetFloat(_dissolveAmount, v), 0.7f, 2.5f).SetEase(Ease.Linear))
                .AppendCallback(() => _sampleMaterial.SetFloat(_blinkValue, 1))
                .Append(DOTween.To(() => _sampleMaterial.GetFloat(_blinkValue), v => _sampleMaterial.SetFloat(_blinkValue, v), 0, 0.15f))
                .JoinCallback(() => 
                {
                    _animator.SetTrigger(_blinkTriggerHash);
                    IsSetupEnd = true;
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
            _isCanceling = true;
            _sampleMaterial.SetFloat(_blinkTriggerHash, 0);
            _unsetTween = DOTween.To(() => _sampleMaterial.GetFloat(_dissolveAmount), v => _sampleMaterial.SetFloat(_dissolveAmount, v), 0, 0.2f)
                .OnComplete(() => _isCanceling = false);
            IsSetupEnd = false;
        }

        public override void Attack()
        {
            _pivotTrm.DOLocalRotate(new Vector3(0, 0, -380f), 0.25f, RotateMode.FastBeyond360).OnComplete(() => UnsetWeapon());
            CameraManager.Instance.ShakeCamera(5, 3, 0.3f);
        }
    }
}
