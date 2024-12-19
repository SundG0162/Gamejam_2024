using DG.Tweening;
using UnityEngine;

namespace BSM.UI
{
    public class TagIconUI : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        public RectTransform RectTrm => transform as RectTransform;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetActive(bool isActive)
        {
            float alpha = isActive ? 1.0f : 0.5f;
            float scale = isActive ? 150f : 100f;
            RectTrm.sizeDelta = new Vector2(scale, scale);
            _canvasGroup.DOFade(alpha, 0.3f);
        }
    }
}
