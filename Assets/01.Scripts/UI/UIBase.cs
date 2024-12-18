using DG.Tweening;
using UnityEngine;

namespace BSM
{
    public enum EUIState
    {
        Opening,
        Opened,
        Closing,
        Closed
    }
    public abstract class UIBase : MonoBehaviour
    {
        public RectTransform RectTrm => transform as RectTransform;

        private CanvasGroup _canvasGroup;

        protected EUIState _uiState = EUIState.Closed;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Close();
        }

        public abstract void Open();
        public abstract void Close();

        public void SetActive(bool isActive, float duration = 0)
        {
            float alpha = isActive ? 1f : 0;
            _canvasGroup.DOFade(alpha, duration);
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = !isActive;
        }
    }
}
