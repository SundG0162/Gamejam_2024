using BSM.Animators;
using DG.Tweening;
using UnityEngine;

namespace BSM.Entities
{
    public class EntityRenderer : AnimateRenderer, IEntityComponent
    {
        private Entity _entity;
        private SpriteRenderer _spriteRenderer;

        private Material _sampleMaterial;
        private readonly int _blinkValueID = Shader.PropertyToID("_BlinkValue");
        private readonly int _dissolveAmountID = Shader.PropertyToID("_DissolveAmount");
        private readonly int _teleportAmountID = Shader.PropertyToID("_TeleportAmount");

        private Tween _blinkTween;
        private Tween _dissolveTween;
        private Sequence _teleportSequence;

        [field: SerializeField]
        public float FacingDirection { get; private set; } = 1;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sampleMaterial = _spriteRenderer.material;
        }

        public void Flip()
        {
            FacingDirection *= -1;
            gameObject.transform.Rotate(new Vector3(0, 180f, 0));
        }

        public void Flip(float x)
        {
            if (Mathf.Abs(FacingDirection + x) < 0.5f)
                Flip();
        }

        public void Blink(float time = 0.15f)
        {
            if (_blinkTween != null && _blinkTween.IsActive())
                _blinkTween.Kill();
            _sampleMaterial.SetFloat(_blinkValueID, 1f);
            _blinkTween = DOTween.To(() => _sampleMaterial.GetFloat(_blinkValueID), v => _sampleMaterial.SetFloat(_blinkValueID, v), 0, time);
        }

        public void Dissolve(float endValue, float time)
        {
            if (_dissolveTween != null && _dissolveTween.IsActive())
                _dissolveTween.Kill();
            _dissolveTween = DOTween.To(() => _sampleMaterial.GetFloat(_dissolveAmountID), v => _sampleMaterial.SetFloat(_dissolveAmountID, v), endValue, time);
        }

        public void Disappear(float time)
        {
            if (_teleportSequence != null && _teleportSequence.IsActive())
                _teleportSequence.Kill();
            _teleportSequence = DOTween.Sequence();
            _teleportSequence
                .Append(DOTween.To(() => _sampleMaterial.GetFloat(_blinkValueID), v => _sampleMaterial.SetFloat(_blinkValueID, v), 1, time))
                .Append(DOTween.To(() => _sampleMaterial.GetFloat(_teleportAmountID), v => _sampleMaterial.SetFloat(_teleportAmountID, v), 2, time))
                .Join(_spriteRenderer.DOFade(0, time + 0.05f))
                .Join(transform.DOLocalMoveY(0.5f, time));

        }

        public void Appear(float time)
        {
            if (_teleportSequence != null && _teleportSequence.IsActive())
                _teleportSequence.Kill();
            _teleportSequence = DOTween.Sequence();
            transform.position = transform.position + Vector3.up * 0.5f;
            _teleportSequence.Append(transform.DOLocalMoveY(0, time))
                .Join(_spriteRenderer.DOFade(1, time))
                .Append(DOTween.To(() => _sampleMaterial.GetFloat(_blinkValueID), v => _sampleMaterial.SetFloat(_blinkValueID, v), 0, time))
                .Join(DOTween.To(() => _sampleMaterial.GetFloat(_teleportAmountID), v => _sampleMaterial.SetFloat(_teleportAmountID, v), -1, time));
        }
    }
}
