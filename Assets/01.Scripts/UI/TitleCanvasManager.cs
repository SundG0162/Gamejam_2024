using System;
using SSH;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using BSM.Core.Audios;

namespace SSH.UI
{
    public class TitleCanvasManager : MonoBehaviour
    {
        [SerializeField]private OpenableCanvas SettingCanvas;
        public string GameSceneName;
        public string TutorialSceneName;
        [SerializeField] private Image _changeEffect;
        private static readonly int Center = Shader.PropertyToID("_Center");

        private void Awake()
        {
            _changeEffect.material.SetFloat(Center,-4f);
            Screen.SetResolution(1920, 1080, true);
        }

        private void Start()
        {
            AudioManager.Instance.PlayAudio("TitleBGM", true);
        }

        public void StartGame()
        {
            AudioManager.Instance.PlayAudio("SceneTransition");
            _changeEffect.material.SetFloat(Center, -4f);
            DontDestroyOnLoad(_changeEffect.transform.parent.gameObject);

            _changeEffect.material.DOFloat(1f, Center, 0.5f)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(GameSceneName);

                    _changeEffect.material.DOFloat(6f, Center, 0.5f)
                        .OnComplete(() =>
                        {
                            Destroy(_changeEffect.transform.parent.gameObject);
                        });
                });

        }

        public void StartTutorial()
        {
            AudioManager.Instance.PlayAudio("SceneTransition");
            _changeEffect.material.SetFloat(Center, -4f);
            DontDestroyOnLoad(_changeEffect.transform.parent.gameObject);

            _changeEffect.material.DOFloat(1f, Center, 0.5f)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(TutorialSceneName);

                    _changeEffect.material.DOFloat(6f, Center, 0.5f)
                        .OnComplete(() =>
                        {
                            Destroy(_changeEffect.transform.parent.gameObject);
                        });
                });
        }

        public void EndGame()
        {
            Application.Quit();
        }
    }
}
