using System;
using BSM.Inputs;
using UnityEngine;
using DG.Tweening;

namespace BSM
{
    public class StatUI : MonoBehaviour
    {
        public enum UIStatus
        {
            Closed, Opening, Open, Closing
        }

        [SerializeField] private InputReaderSO _playerInput;
        private CanvasGroup _canvasGroup;

        private UIStatus _uiStatus = UIStatus.Closed;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        private void OpenCanvas(float time = 0.5f)
        {
            _canvasGroup.DOFade(1f, time);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
        private void closeCanvas(float time = 0.5f)
        {
            _canvasGroup.DOFade(0f, time);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
    }
}
