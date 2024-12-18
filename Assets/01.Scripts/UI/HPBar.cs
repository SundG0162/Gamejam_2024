using BSM.Entities;
using DG.Tweening;
using System;
using UnityEngine;

namespace BSM.UI
{
    public class HPBar : Bar
    {
        [SerializeField]
        private EntityHealth _entityHealth;

        private Material _sampleMaterial;
        private readonly int _blinkValueID = Shader.PropertyToID("_BlinkValue");

        [SerializeField]
        private Transform _whitePivotTrm;

        private Sequence _barSequence;
        private Sequence _whiteBarSequence;

        protected override void Awake()
        {
            base.Awake();
            _entityHealth.OnHealthChangeEvent += HandleOnHealthChangeEvent;
            _sampleMaterial = _pivotTrm.Find("Bar").GetComponent<SpriteRenderer>().material;
            _whitePivotTrm = transform.Find("WhitePivot");
        }


        public void OnAfterInitialize()
        {
            _entityHealth.OnHealthChangeEvent += HandleOnHealthChangeEvent;
        }

        private void HandleOnHealthChangeEvent(float prevHealth, float currentHealth)
        {
            if (_barSequence != null && _barSequence.IsActive())
                _barSequence.Kill();
            if (_whiteBarSequence != null && _whiteBarSequence.IsActive())
                _whiteBarSequence.Kill();
            float ratio = currentHealth / _entityHealth.MaxHealth;
            _whiteBarSequence = DOTween.Sequence();
            _whiteBarSequence
                .AppendInterval(0.8f)
                .Append(DOTween.To(() => _whitePivotTrm.transform.localScale.x, v => _whitePivotTrm.transform.localScale = new Vector2(v,1), ratio, 0.3f));
            _sampleMaterial.SetFloat(_blinkValueID, 1);
            _barSequence = DOTween.Sequence();
            _barSequence
                .Append(DOTween.To(() => FillAmount, v => FillAmount = v, ratio, 0.3f))
                .Join(DOTween.To(() => _sampleMaterial.GetFloat(_blinkValueID), v => _sampleMaterial.SetFloat(_blinkValueID, v), 0, 0.25f));
            FillAmount = ratio;
        }
    }
}
