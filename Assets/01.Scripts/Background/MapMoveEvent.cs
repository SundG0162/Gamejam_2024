using System;
using UnityEngine;

namespace SSH
{
    public class MapMoveEvent : MonoBehaviour
    {
        private InfiniteBackground _background;
        public Vector3 _moveDirAmount;

        private void Awake()
        {
            //_moveDirAmount = transform.position;
            _background = GameObject.Find("BackgroundMap").GetComponentInChildren<InfiniteBackground>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            print("triggerenter");
            _background.MovePosition(_moveDirAmount);
        }
    }
}
