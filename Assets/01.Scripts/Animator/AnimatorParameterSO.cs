using UnityEngine;

namespace BSM.Animators
{
    [CreateAssetMenu(menuName = "SO/Animators/AnimatorParameter")]
    public class AnimatorParameterSO : ScriptableObject
    {
        public string parameterName;
        public int hashValue;

        private void OnValidate()
        {
            hashValue = Animator.StringToHash(parameterName);
        }
    }
}
