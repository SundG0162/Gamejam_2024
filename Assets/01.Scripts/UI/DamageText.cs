using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BSM.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _text, _shadow;
        private Sequence _popupSequence;

        public void Initialize(float damage)
        {
            if (Mathf.Approximately(damage, float.MaxValue))
                _text.text = "¡Ä";
            else
                _text.text = Mathf.CeilToInt(damage).ToString();
            _shadow.text = _text.text;
            _popupSequence = DOTween.Sequence();
            transform.localScale = Vector3.one;
            _popupSequence
                .Append(transform.DOScale(0, 0.4f).SetEase(Ease.InCubic))
                .Join(transform.DOMoveY(transform.position.y + 0.5f, 0.4f).SetEase(Ease.InCubic));
        }
    }
}
