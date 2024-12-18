using BSM.Entities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BSM.UI
{
    public class PlayerHPBarUI : UIBase
    {
        [SerializeField]
        private EntityHealth _entityHealth;
        [SerializeField]
        private SlicedFilledImage _barImage;

        protected override void Awake()
        {
            base.Awake();
            _entityHealth.OnHealthChangeEvent += HandleOnHealthChangeEvent;
            Open();
        }

        private void HandleOnHealthChangeEvent(float prevHealth, float currentHealth)
        {
            _barImage.fillAmount = currentHealth / _entityHealth.MaxHealth;
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
