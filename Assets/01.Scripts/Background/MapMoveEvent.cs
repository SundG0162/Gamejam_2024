using BSM.Players;
using UnityEngine;

namespace SSH
{
    public class MapMoveEvent : MonoBehaviour
    {
        [SerializeField]
        private InfiniteBackground _background;
        public Vector3 _moveDirAmount;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
                _background.MovePosition(_moveDirAmount);
        }
    }
}
