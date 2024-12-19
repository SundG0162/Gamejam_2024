using BSM.Players;
using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace BSM.UI
{
    public class PlayerSelectPanel : UIBase
    {
        public bool IsSelectEnd { get; private set; } = false;
        [SerializeField]
        private SelectUI[] _selectUIs;
        [SerializeField]
        private SlicedFilledImage _barImage;
        public EPlayerType Result { get; private set; }

        private float _timer = 0f;

        protected override void Awake()
        {
            base.Awake();
            _selectUIs.ToList().ForEach(ui => ui.OnSelectEvent += HandleOnSelectEvent);
        }

        private void Update()
        {
            if(_uiState == EUIState.Opened)
            {
                float ratio = _timer / 5f;
                _timer -= Time.unscaledDeltaTime;
                _barImage.fillAmount = ratio;
            }
        }

        public override void Open()
        {
            IsSelectEnd = false;
            _timer = 5f;
            SetActive(true, 0.2f);
        }

        public override void Close()
        {
            SetActive(false, 0.2f);
        }

        public void RandomSelect()
        {
            int rand = Random.Range(0, _selectUIs.Length);
            _selectUIs[rand].OnPointerClick(null);
        }

        private void HandleOnSelectEvent(EPlayerType type)
        {
            IsSelectEnd = true;
            Result = type;
            Close();
        }
    }
}
