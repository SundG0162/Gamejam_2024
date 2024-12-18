using UnityEngine;

namespace BSM.UI
{
    public class Bar : MonoBehaviour
    {
        protected Transform _pivotTrm;

        public float FillAmount
        {
            get => _pivotTrm.transform.localScale.x;
            set
            {
                value = Mathf.Clamp01(value);
                _pivotTrm.transform.localScale = new Vector2(value, 1);
            }
        }

        protected virtual void Awake()
        {
            _pivotTrm = transform.Find("Pivot");
        }
    }
}
