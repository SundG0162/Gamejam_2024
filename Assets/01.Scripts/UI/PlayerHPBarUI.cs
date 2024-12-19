using BSM.Entities;
using BSM.Players;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BSM.UI
{
    public class PlayerHPBarUI : UIBase
    {
        [SerializeField]
        private PlayerTag _playerTag;
        private EntityHealth _entityHealth;
        [SerializeField]
        private SlicedFilledImage _barImage, _whiteBarImage;
        private Material _blinkMaterial;

        private readonly int _blinkValueID = Shader.PropertyToID("_BlinkValue");

        private Sequence _barSequence;
        private Sequence _whiteBarSequence;

        protected override void Awake()
        {
            base.Awake();
            _playerTag.OnPlayerChangeEvent += HandleOnPlayerChangeEvent;
            _blinkMaterial = _barImage.material;
            Open();
        }

        private void HandleOnPlayerChangeEvent(Player player)
        {
            if (_entityHealth != null)
                _entityHealth.OnHealthChangeEvent -= HandleOnHealthChangeEvent;
            _entityHealth = player.GetEntityComponent<EntityHealth>();
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
                .Append(DOTween.To(() => _whiteBarImage.fillAmount, v => _whiteBarImage.fillAmount = v, ratio, 0.3f));
            _blinkMaterial.SetFloat(_blinkValueID, 1);
            _barSequence = DOTween.Sequence();
            _barSequence
                .Append(DOTween.To(() => _barImage.fillAmount, v => _barImage.fillAmount = v, ratio, 0.15f))
                .Join(DOTween.To(() => _blinkMaterial.GetFloat(_blinkValueID), v => _blinkMaterial.SetFloat(_blinkValueID, v), 0, 0.15f));
        }

        public override void Open()
        {
            SetActive(true);
        }

        public override void Close()
        {
            SetActive(false);
        }
    }
}
