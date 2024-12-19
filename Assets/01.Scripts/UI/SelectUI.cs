using BSM.Players;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BSM.UI
{
    public class SelectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private Image _image;
        private Color _originColor;
        private Tween _colorChangeTween;
        [SerializeField]
        private EPlayerType _playerType;

        public event Action<EPlayerType> OnSelectEvent;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _originColor = _image.color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _image.color = Color.white;
            _image.DOColor(_originColor, 0.1f).SetUpdate(true);
            OnSelectEvent?.Invoke(_playerType);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_colorChangeTween != null && _colorChangeTween.IsActive())
                _colorChangeTween.Kill();
            Color solid = _originColor;
            solid.a = _originColor.a + 0.2f;
            _colorChangeTween = _image.DOColor(solid, 0.3f).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_colorChangeTween != null && _colorChangeTween.IsActive())
                _colorChangeTween.Kill();
            _colorChangeTween = _image.DOColor(_originColor, 0.3f).SetUpdate(true);
        }
    }
}
