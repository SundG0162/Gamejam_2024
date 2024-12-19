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
            if (_text.text == "¡Ä")
                transform.localScale = Vector2.one * 1.5f;
            else
                transform.localScale = new Vector2(0.3f, 0.3f) + Vector2.one * (damage / 4f);
            _popupSequence
                .Append(transform.DOScale(0, 0.4f).SetEase(Ease.InCubic))
                .Join(transform.DOMoveY(transform.position.y + 0.5f, 0.4f).SetEase(Ease.InCubic));
        }
    }
}
