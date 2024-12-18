using SSH;
using UnityEngine;
using UnityEngine.UI;

namespace BSM
{
    public class SettingCanvasManager : OpenableCanvas
    {
        [SerializeField]private Slider volumeSlider;

        public void OpenSettingCanvas()
        {
            OpenCanvas();
        }
        
        public void SaveAndClose()
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
            print($"{PlayerPrefs.GetFloat("Volume")} is current volume");
            CloseCanvas();
        }
    }
}
