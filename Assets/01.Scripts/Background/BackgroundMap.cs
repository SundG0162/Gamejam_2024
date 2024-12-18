using System;
using UnityEngine;

namespace SSH
{
    public class BackgroundMap : MonoBehaviour
    {
        public Vector3 _size = new Vector3(22, 22, 1);
        public Vector3 _offset = new Vector3(0,0,0);
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position + _offset, _size);
        }
    }
}
