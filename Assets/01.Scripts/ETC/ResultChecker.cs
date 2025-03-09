using UnityEngine;

namespace BSM.ETC
{
    public class ResultChecker : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.SetInt("CaughtEnemy", 0);
            PlayerPrefs.SetFloat("AliveTime", 0);
        }

        private void Update()
        {
            PlayerPrefs.SetFloat("AliveTime", PlayerPrefs.GetFloat("AliveTime") + Time.deltaTime);
        }
    }
}
