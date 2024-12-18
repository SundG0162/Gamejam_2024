using SSH;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace SSH.UI
{
    public class TitleCanvasManager : MonoBehaviour
    {
        [SerializeField]private OpenableCanvas SettingCanvas;
        public string GameSceneName;
        [SerializeField] private Image _changeEffect;
        
        public void StartGame()
        {
            
            _changeEffect.material.DOFloat(1f, "_Center", 0.5f)
                .OnComplete(()=>SceneManager.LoadScene(GameSceneName));
        }

        public void EndGame()
        {
            Application.Quit();
        }
    }
}
