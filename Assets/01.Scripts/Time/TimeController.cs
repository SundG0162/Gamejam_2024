using BSM.Core;
using System;
using System.Collections;
using UnityEngine;

namespace BSM.Times
{
    public class TimeController : MonoSingleton<TimeController>
    {
        public void SetTimeFreeze(float freezeValue, float beforeDelay, float freezeTime)
        {
            StopAllCoroutines();

            StartCoroutine(TimeFreezeCoroutine(freezeValue, beforeDelay, () =>
            {
                StartCoroutine(TimeFreezeCoroutine(1f, freezeTime));
            }));
        }

        private IEnumerator TimeFreezeCoroutine(float freezeValue, float beforeDelay, Action Callback = null)
        {
            yield return new WaitForSecondsRealtime(beforeDelay);
            Time.timeScale = freezeValue;
            Callback?.Invoke();
        }
    }
}
