using BSM.Players;
using System;
using UnityEngine;

namespace BSM.UI
{
    public class PlayerManaUI : UIBase
    {
        [SerializeField]
        private PlayerTag _playerTag;
        [SerializeField]
        private SlicedFilledImage _barImage;

        protected override void Awake()
        {
            base.Awake();
            Open();
        }


        public override void Open()
        {
            SetActive(true);
            _playerTag.OnManaChangeEvent += HandleOnManaChangeEvent;
        }

        private void HandleOnManaChangeEvent(float prevValue, float currentValue)
        {
            _barImage.fillAmount = currentValue / _playerTag.MaxMana;
        }

        public override void Close()
        {
            SetActive(false);
            _playerTag.OnManaChangeEvent -= HandleOnManaChangeEvent;
        }
    }
}
