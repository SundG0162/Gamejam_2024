using UnityEngine;

namespace BSM.UI
{
    public class Bar : MonoBehaviour
    {
        private Transform _pivotTrm;

        private void Awake()
        {
            _pivotTrm = transform.Find("Pivot");
        }

        public void SetFillAmount(float fillAmount)
        {
            _pivotTrm.transform.localScale = new Vector2(fillAmount, 1);
        }
    }
}
