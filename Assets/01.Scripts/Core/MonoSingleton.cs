using UnityEngine;

namespace BSM.Core
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        Debug.LogError($"¥‘ ΩÃ±€≈Ê æ»≥÷¿∏Ω… : {typeof(T).ToString()}");
                    }
                }
                return _instance;
            }
        }
    }
}
