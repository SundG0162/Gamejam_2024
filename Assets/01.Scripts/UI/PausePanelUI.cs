using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BSM.Inputs;
using System;
using BSM.Players;

namespace BSM.UI
{
    public class PausePanelUI : UIBase
    {
        [SerializeField]
        private Button _continueBtn, _exitBtn;

        [SerializeField]
        private PlayerTag _playerTag;

        [SerializeField]
        private InputReaderSO _inputReader;

        protected override void Awake()
        {
            base.Awake();
            _continueBtn.onClick.AddListener(HandleOnContinueButtonClick);
            _exitBtn.onClick.AddListener(HandleOnExitButtonClick);
            _inputReader.OnPauseEvent += HandleOnPauseEvent;
            _playerTag.OnDeadEvent += HandleOnDeadEvent;
        }

        private void HandleOnDeadEvent()
        {
            _inputReader.OnPauseEvent -= HandleOnPauseEvent;
        }

        private void HandleOnPauseEvent()
        {
            if (_uiState == EUIState.Closed)
                Open();
            else
                Close();
        }

        private void HandleOnContinueButtonClick()
        {
            Close();
        }

        private void HandleOnExitButtonClick()
        {
            Close();
            SceneManager.LoadScene("TitleScene");
        }

        public override void Open()
        {
            Time.timeScale = 0f;
            SetActive(true, 0.3f);
        }

        public override void Close()
        {
            SetActive(false, 0.3f);
            Time.timeScale = 1f;
        }
    }
}
