using UnityEngine;

namespace BSM.Animators
{
    public class AnimateRenderer : MonoBehaviour
    {
        [SerializeField]
        protected Animator _animator;

        public void SetParam(AnimatorParameterSO parameter, bool value) => _animator.SetBool(parameter.hashValue, value);
        public void SetParam(AnimatorParameterSO parameter, float value) => _animator.SetFloat(parameter.hashValue, value);
        public void SetParam(AnimatorParameterSO parameter, int value) => _animator.SetInteger(parameter.hashValue, value);
        public void SetParam(AnimatorParameterSO parameter) => _animator.SetTrigger(parameter.hashValue);
    }
}
