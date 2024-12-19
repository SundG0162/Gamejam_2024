using BSM.Inputs;
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
        private TextMeshProUGUI _descText;

        private void Update()
        {
            if(_uiState == EUIState.Opened)
            {
                if(Keyboard.current.escapeKey.wasPressedThisFrame)
                {
                    Close();
                    SceneManager.LoadScene("TitleScene");
                }
            }
        }

        public override void Close()
        {
            Time.timeScale = 1f;
            SetActive(false);
        }

        public override void Open()
        {
            Time.timeScale = 0f;
            SetActive(true);
            _descText.text = $"����� {PlayerPrefs.GetInt("CaughtEnemy")}������ ���� óġ�߰�,\n{PlayerPrefs.GetFloat("AliveTime")}�ʵ��� ��Ƴ��ҽ��ϴ�.";
            RectTrm.anchoredPosition = new Vector2(1920, 0);
            RectTrm.DOAnchorPos(Vector2.zero, 0.7f).SetEase(Ease.InQuad);
        }
    }
}
