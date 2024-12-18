using DG.Tweening;
using UnityEngine;

namespace BSM.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        private SpriteRenderer _spriteRenderer;
        private Material _sampleMaterial;
        private readonly int _blinkValueID = Shader.PropertyToID("_BlinkValue");
        private readonly int _dissolveAmountID = Shader.PropertyToID("_DissolveAmount");

        private Tween _blinkTween;
        private Tween _dissolveTween;

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
            _entity.transform.Rotate(new Vector3(0, 180f, 0));
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
    }
}
