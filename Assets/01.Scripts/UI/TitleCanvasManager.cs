using SSH;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

namespace SSH.UI
{
    public class TitleCanvasManager : MonoBehaviour
    {
        [SerializeField]private OpenableCanvas SettingCanvas;
        public string GameSceneName;
        
        public void StartGame()
        {
            SceneManager.LoadScene(GameSceneName);
        }

        public void EndGame()
        {
            Application.Quit();
        }
    }
}
