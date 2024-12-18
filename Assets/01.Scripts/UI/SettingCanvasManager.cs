using SSH;
using UnityEngine;
using UnityEngine.UI;

namespace SSH.UI
{
    public class SettingCanvasManager : OpenableCanvas
    {
        [SerializeField]private Slider volumeSlider;

        public void OpenSettingCanvas()
        {
            OpenCanvas();
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        
        public void SaveAndClose()
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
            print($"{PlayerPrefs.GetFloat("Volume")} is current volume");
            CloseCanvas();
        }
    }
}
