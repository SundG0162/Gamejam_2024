using UnityEngine;

namespace BSM
{
    public class TMPSortingLayer : MonoBehaviour
    {
        [SerializeField]
        private string _sortingLayerName;
        [SerializeField]
        private int _sortingOrder;

        private void Awake()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.sortingLayerName = _sortingLayerName;
            renderer.sortingOrder = _sortingOrder;
        }
    }
}
