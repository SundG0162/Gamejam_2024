using BSM.Core.Cameras;
using BSM.Inputs;
using BSM.Players;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BSM.UI
{
    public class ResultPanelUI : UIBase
    {
        [SerializeField]
        private PlayerTag _playerTag;
        [SerializeField]
        private TextMeshProUGUI _descText;

        [SerializeField]
        private InputReaderSO _inputReader;

        private void Start()
        {
            _playerTag.OnDeadEvent += HandleOnDeadEvent;
            _inputReader.OnPauseEvent += HandleOnPauseEvent;
        }

        private void HandleOnPauseEvent()
        {
            if (_uiState != EUIState.Closed)
            {
                Close();
                SceneManager.LoadScene("TitleScene");
            }
        }

        private void HandleOnDeadEvent()
        {
            Time.timeScale = 0;
            CameraManager.Instance.CurrentCam.Follow = null;
            CameraManager.Instance.TiltCamera(-25f, 0.3f, Ease.OutQuint, true);
            CameraManager.Instance.Zoom(1, 0.5f, Ease.InBack, true, () =>
            {
                CameraManager.Instance.Zoom(3, 0.5f, Ease.OutCirc, true);
                CameraManager.Instance.TiltCamera(25f, 0.2f, Ease.OutCirc, true, () =>
                {
                    Open();
                });
            });
        }

        public override void Close()
        {
            _inputReader.EnableAllInputs();
            Time.timeScale = 1f;
            SetActive(false);
        }

        public override void Open()
        {
            SetActive(true);
            _descText.text = $"당신은 {PlayerPrefs.GetInt("CaughtEnemy")}마리의 적을 처치했고,\n{PlayerPrefs.GetFloat("AliveTime")}초동안 살아남았습니다.";
            RectTrm.anchoredPosition = new Vector2(1920, 0);
            RectTrm.DOAnchorPos(Vector2.zero, 0.7f).SetEase(Ease.InQuad).SetUpdate(true);
        }
    }
}
