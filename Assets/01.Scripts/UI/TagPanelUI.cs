using BSM.Players;
using BSM.Utils;
using DG.Tweening;
using System;
using UnityEngine;

namespace BSM.UI
{
    public class TagPanelUI : UIBase
    {
        [SerializeField]
        private PlayerTag _playerTag;
        [SerializeField]
        private Vector2[] _positions;
        [SerializeField]
        private TagIconUI[] _icons;
        private int _currentIndex;
        private int _CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                if (_currentIndex > 2)
                    _currentIndex = 0;
                if (_currentIndex < 0)
                    _currentIndex = 2;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _positions = new Vector2[3];
            for (int i = 0; i < _icons.Length; i++)
            {
                _positions[i] = _icons[i].RectTrm.anchoredPosition;
            }
            Open();
        }

        private void Pull()
        {
            _CurrentIndex--;
            for (int i = 0; i < _icons.Length; i++)
            {
                if (i == _CurrentIndex)
                    _icons[_CurrentIndex].SetActive(true);
                else
                    _icons[i].SetActive(false);
            }
            _positions.PullArray(1);
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].RectTrm.DOAnchorPos(_positions[i], 0.5f);
            }
        }

        private void Push()
        {
            _CurrentIndex++;
            for (int i = 0; i < _icons.Length; i++)
            {
                if (i == _CurrentIndex)
                    _icons[_CurrentIndex].SetActive(true);
                else
                    _icons[i].SetActive(false);
            }
            _positions.PushArray(1);
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].RectTrm.DOAnchorPos(_positions[i], 0.5f);
            }
        }

        public override void Open()
        {
            SetActive(true);
            _playerTag.OnTagEvent += HandleOnTagEvent;
        }

        private void HandleOnTagEvent(int roughVariable)
        {
            if (roughVariable == 1)
                Push();
            else
                Pull();
        }

        public override void Close()
        {
            SetActive(false);
            _playerTag.OnTagEvent += HandleOnTagEvent;
        }
    }
}
