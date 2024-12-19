using BSM.Players;
using System;
using UnityEngine;

namespace BSM.UI
{
    public class TagPanelUI : UIBase
    {
        [SerializeField]
        private PlayerTag _playerTag;

        private void Pull()
        {

        }

        private void Push()
        {

        }

        public override void Open()
        {
            SetActive(true);
            _playerTag.OnTagEvent += HandleOnTagEvent;
        }

        private void HandleOnTagEvent(int roughVariable)
        {
        }

        public override void Close()
        {
            SetActive(false);
            _playerTag.OnTagEvent += HandleOnTagEvent;
        }
    }
}
