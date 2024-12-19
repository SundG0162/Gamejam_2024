using BSM.Core.Cameras;
using BSM.Times;
using BSM.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace BSM.Players.ArmorPlayer
{
    public class ArmorPlayer : Player
    {
        private float _tagTime = 5f;

        private PlayerSelectPanel _playerSelectPanel;

        protected override void Awake()
        {
            base.Awake();
            _playerSelectPanel = FindFirstObjectByType<PlayerSelectPanel>();
        }


        public override void Join()
        {
            base.Join();
            StartCoroutine(DelayTagCoroutine());
        }

        public override void Quit()
        {
            base.Quit();
            StopAllCoroutines();
        }

        private IEnumerator DelayTagCoroutine()
        {
            yield return new WaitForSeconds(_tagTime);
            InputReader.DisablePlayerInput();
            _playerSelectPanel.Open();
            CameraManager.Instance.TiltCamera(45, 4f);
            TimeController.Instance.SetTimeFreeze(0.1f, 0, 5f);
            Tween delay = DOVirtual.DelayedCall(5f, _playerSelectPanel.RandomSelect);
            yield return new WaitUntil(() => _playerSelectPanel.IsSelectEnd);
            delay.Kill();
            EPlayerType result = _playerSelectPanel.Result;
            TimeController.Instance.SetTimeFreeze(1, 0, 0);
            CameraManager.Instance.TiltCamera(0, 0.5f);
            _playerTag.TagPlayer(result, true);
            InputReader.EnablePlayerInput();
        }
    }
}
