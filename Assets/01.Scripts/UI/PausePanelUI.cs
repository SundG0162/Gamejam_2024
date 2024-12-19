using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BSM.UI
{
    public class PausePanelUI : UIBase
    {
        [SerializeField]
        private Button _continueBtn, _exitBtn;

        protected override void Awake()
        {
            base.Awake();
            _continueBtn.onClick.AddListener(HandleOnContinueButtonClick);
            _exitBtn.onClick.AddListener(HandleOnExitButtonClick);
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
