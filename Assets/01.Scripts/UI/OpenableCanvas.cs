using System;
using BSM.Inputs;
using UnityEngine;
using DG.Tweening;

namespace SSH.UI
{
    public class OpenableCanvas : MonoBehaviour
    {
        public enum CanvasStatus
        {
            Closed, Opening, Opened, Closing
        }

        //[SerializeField] private InputReaderSO _playerInput;
        private CanvasGroup _canvasGroup;

        private CanvasStatus _canvasStatus = CanvasStatus.Closed;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ChangeCanvas(float time = 1f)
        {
            if(_canvasStatus == CanvasStatus.Closed) OpenCanvas(time);
            else if(_canvasStatus == CanvasStatus.Opened) CloseCanvas(time);
        }

        protected void OpenCanvas(float time = 0.5f)
        {
            _canvasGroup.DOFade(1f, time);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasStatus = CanvasStatus.Opened;
        }
        
        protected void CloseCanvas(float time = 0.5f)
        {
            _canvasGroup.DOFade(0f, time);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasStatus = CanvasStatus.Closed;
        }
        
    }
}
